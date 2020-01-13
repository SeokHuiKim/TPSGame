using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunController : GunController
{
    public static bool isActive = false;

    private readonly string railEffect1 = "RailBoom";
    private readonly string railEffect2 = "GatlingBoom";

    // 무기에 따른 상태변수

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
        PlayerAnimation.Instance.RailGun_Attack_Animation();
        RailShooting();
    }

    private void RailShooting()
    {
        if (Physics.SphereCast(cameraRay.origin, 1f, cameraRay.direction, out RaycastHit hit, _range, layerMask[0] + layerMask[1], QueryTriggerInteraction.Ignore))
        {
            GameObject effect = ObjectPool.Instance.PopFromPool(railEffect1);
            effect.transform.position = hit.point;
            effect.SetActive(true);

            GameObject effect2 = ObjectPool.Instance.PopFromPool(railEffect2);
            effect2.transform.position = hit.point;
            effect2.SetActive(true);

            CauseDamage(hit, false);
        }
    }
}