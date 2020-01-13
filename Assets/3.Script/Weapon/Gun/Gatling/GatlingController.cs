using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingController : GunController
{
    public static bool isActive = false;

    private readonly string gatlingBoom = "GatlingBoom";
    private float gatlingMuzzleLifeTime;

    private Transform gatlingPos;
    public GameObject gatlingMuzzle;

    void Start()
    {
        gatlingPos = GameObject.Find("GETT").transform;
        gatlingMuzzleLifeTime = gatlingMuzzle.GetComponent<ParticleSystem>().main.startLifetime.constant;
        gatlingMuzzle.SetActive(false);
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

    private void LateUpdate()
    {
        if (isActive) gatlingPos.Rotate(new Vector3(0, -210, 180));
        else gatlingPos.Rotate(new Vector3(0, -180, 180));
    }

    protected override void Shoot()
    {
        base.Shoot();
        StartCoroutine(MuzzleLifeTime());
        GalitngShooting();
    }

    private void GalitngShooting()
    {
        if (Physics.SphereCast(cameraRay.origin, 1f, cameraRay.direction, out RaycastHit hit, _range, layerMask[0] + layerMask[1], QueryTriggerInteraction.Ignore))
        {
            GameObject gatlingEffect = ObjectPool.Instance.PopFromPool(gatlingBoom);
            if (gatlingEffect != null)
            {
                gatlingEffect.transform.position = hit.point;
                gatlingEffect.SetActive(true);
            }

            CauseDamage(hit, false);
        }
    }

    private IEnumerator MuzzleLifeTime()
    {
        gatlingMuzzle.SetActive(true);
        float lifeTime = 0.1f;
        while(lifeTime > 0)
        {
            yield return null;
            lifeTime -= Time.deltaTime;
        }
        gatlingMuzzle.SetActive(false);
    }
}

