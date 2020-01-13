using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileController : GunController
{
    public static bool isActive = false;

    private readonly string _missileBullet = "MissileBullet";
    private readonly string _missileLaunch = "MissileLaunchSmoke";

    public GameObject hitObject;
    public Transform[] muzzlePosition;
    public GameObject player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
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
        PlayerAnimation.Instance.Missile_Attack_Animation();
        MissileShoot();
    }

    private void MissileShoot()
    {
        if (Physics.SphereCast(cameraRay.origin, 3f, cameraRay.direction, out RaycastHit hit, _range, layerMask[0] + layerMask[1], QueryTriggerInteraction.Ignore))
        {
            if (hit.transform.CompareTag("Boss") || hit.transform.CompareTag("Enemy"))
            {
                hitObject = hit.collider.gameObject;
                MissileCreate(hit.point);
            }
            else
            {
                hitObject = null;
                MissileCreate(hit.point);
            }
        }
        else
        {
            hitObject = null;
            MissileCreate(cameraRay.direction * _range);
        }
    }

    private void MissileCreate(Vector3 _pos)
    {
        GameObject[] missile = new GameObject[8];
        GameObject[] missileLaunch = new GameObject[8];
        for (int i = 0; i < 8; i++)
        {
            missile[i] = ObjectPool.Instance.PopFromPool(_missileBullet);
            missile[i].GetComponent<Missile>().SetDirection(muzzlePosition[i].position, (_pos - muzzlePosition[i].position).normalized);
            missile[i].transform.LookAt(_pos);
            missile[i].SetActive(true);

            missileLaunch[i] = ObjectPool.Instance.PopFromPool(_missileLaunch);
            missileLaunch[i].transform.position = muzzlePosition[i].position;
            missileLaunch[i].SetActive(true);
        }
    }
}