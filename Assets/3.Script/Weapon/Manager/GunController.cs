using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GunController : MonoBehaviour
{
    private WeaponSlot slot;
    private bool isReload;
    private float currneFireRate;

    protected int[] layerMask;
    protected readonly float _range = 500f;
    protected Ray cameraRay;
    protected bool isRailFire;

    private bool isHit;
    public Gun currentGun;

    private void Awake()
    {
        slot = GameObject.FindGameObjectWithTag("WeaponSlot").GetComponent<WeaponSlot>();
        layerMask = new int[2];
        layerMask[0] = (-1) - (1 << 9);
        layerMask[1] = (-1) - (1 << 14);
    }

    protected void FireRateCalc()
    {
        if (currneFireRate > 0)
            currneFireRate -= Time.deltaTime;
    }

    protected void TryFire()
    {
        if (GatlingController.isActive)
        {
            if (Input.GetMouseButton(0) && currneFireRate <= 0 && !isHit)
            {
                Fire();
            }
        }
        else
        {
            if (Input.GetMouseButtonDown(0) && currneFireRate <= 0 && !isHit)
            {
                Fire();
            }
        }
    }

    protected void Fire()
    {
        if (!isReload)
        {
            if (currentGun.currentBullet > 0)
                Shoot();
            else
                StartCoroutine(ReloadCoroutine());
        }
    }

    protected virtual void Shoot()
    {
        currentGun.currentBullet--;
        currneFireRate = currentGun.fireRate;
        SoundManager.Instance.PlaySFX(currentGun.audioclip);
    }

    protected void TryReload()
    {
        if (Input.GetKeyDown(KeyCode.R) || currentGun.currentBullet <= 0 && !isReload)
            StartCoroutine(ReloadCoroutine());
    }

    protected IEnumerator ReloadCoroutine()
    {
        slot.Appear();
        if (currentGun.currentBullet < currentGun.reloadBullet)
        {
            isReload = true;

            currentGun.currentBullet = 0;

            yield return new WaitForSeconds(currentGun.reloadTime);
            currentGun.currentBullet = currentGun.reloadBullet;
            isReload = false;
        }
        else
            Debug.Log("총알이 최대치입니다.");
    }

    protected void RayCastCenter()
    {
        cameraRay = Camera.main.ScreenPointToRay(new Vector3(Camera.main.pixelWidth / 2, Camera.main.pixelHeight / 2));
    }

    protected void CauseDamage(RaycastHit hit, bool _down)
    {
        if (hit.transform.CompareTag("Boss"))
        {
            hit.transform.GetComponent<BossControl>().Damage(currentGun.damage, _down);
        }
        else if (hit.transform.CompareTag("BossFunnel"))
        {
            hit.transform.GetComponent<BossFunnel>().Damage(currentGun.damage);
        }
        else if (hit.transform.CompareTag("Enemy"))
        {
            hit.transform.GetComponent<EnemyControl>().Damage(currentGun.damage);
        }
    }

    public void SetHit(bool hit)
    {
        isHit = hit;
    }
}