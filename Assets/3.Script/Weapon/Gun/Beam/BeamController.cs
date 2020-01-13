using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamController : GunController
{
    public static bool isActive = false;

    private readonly string beamBoom = "BeamBoom";

    private bool isLineOn;
    private readonly float beamLifeTime = 0.5f;
    private readonly float beamSize = 2f;

    public LineRenderer beamLine;

    void Start()
    {
        beamLine.gameObject.SetActive(false);
    }

    void Update()
    {
        if (isActive)
        {
            FireRateCalc();
            TryFire();
            TryReload();
            RayCastCenter();
        }
    }

    protected override void Shoot()
    {
        base.Shoot();
        PlayerAnimation.Instance.Beam_Attack_Animation();
    }

    private void LineSet(Vector3 _pos)
    {
        beamLine.SetPosition(0, currentGun.muzzle.position);
        beamLine.SetPosition(1, _pos); // 라인 도착점 설정
    }

    private void LineOn(bool _lineOn)
    {
        if (isLineOn != _lineOn) // 현재 상태와 달라진다면
        {
            isLineOn = _lineOn;
            if (isLineOn) beamLine.gameObject.SetActive(true);
            else if (!isLineOn) beamLine.gameObject.SetActive(false);
        }
    }

    private void BeamShot()
    {
        StartCoroutine(BeamLife());
        if (Physics.SphereCast(cameraRay.origin, 1f, cameraRay.direction, out RaycastHit hit, _range, layerMask[0] + layerMask[1], QueryTriggerInteraction.Ignore))
        {
            BeamEffect(hit.point);
            LineSet(hit.point);

            CauseDamage(hit, true);
        }
        else LineSet(cameraRay.direction * _range);
    }

    private IEnumerator BeamLife()
    {
        float lifetime = beamLifeTime;
        LineOn(true);
        while (lifetime > 0)
        {
            beamLine.startWidth = (lifetime / beamLifeTime) * beamSize;
            beamLine.endWidth = (lifetime / beamLifeTime) * beamSize;
            lifetime -= Time.deltaTime;
            yield return null;
        }
        LineOn(false);
    }

    private void BeamEffect(Vector3 hitPos)
    {
        GameObject beamEffet = ObjectPool.Instance.PopFromPool(beamBoom);
        beamEffet.transform.position = hitPos;
        beamEffet.SetActive(true);
    }
}
