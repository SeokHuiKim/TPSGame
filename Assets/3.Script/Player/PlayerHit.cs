using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHit : MonoBehaviour
{
    private bool isContinueAnim;
    private int groundGunHitCount, groundSwordHitCount;
    private int airGunHitCount, airSwordHitCount;
    private readonly float downTime = 10f;

    private float posCheck;

    public BoxCollider theCollider;
    public PlayerHUD playerHUD;

    private void Awake()
    {
        groundGunHitCount = groundSwordHitCount = airGunHitCount = airSwordHitCount = 0;
        IsInvincible = false;
        isContinueAnim = false;
    }

    public bool IsInvincible { get; set; }

    public void FrontBackCheck(Transform playerPos, Transform EnemyPos)
    {
        Vector3 frontBackCheck = EnemyPos.TransformPoint(EnemyPos.transform.position) - playerPos.TransformPoint(playerPos.transform.position);
        posCheck = frontBackCheck.z;
    }

    public void GunHit(int count)
    {
        if (Flat.Instance.GetGround())
            groundGunHitCount += count;
        else
            airGunHitCount += count;
    }

    public void SwordHit(int count)
    {
        if (Flat.Instance.GetGround())
            groundSwordHitCount += count;
        else
            airGunHitCount += count;
    }

    public void BeHitSword()
    {
        if (posCheck > 0)
        {
            if (groundSwordHitCount == 1)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitGroundFront);
            }
            else if (groundSwordHitCount == 3 && !isContinueAnim)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.DownGroundFront);
                StartCoroutine(DownInvincible());
            }
        }
        else
        {
            if (groundSwordHitCount == 1)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitGroundBack);
            }
            else if (groundSwordHitCount == 3 && !isContinueAnim)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.DownGroundBack);
                StartCoroutine(DownInvincible());
            }
        }
    }

    public void BeHitGun()
    {
        if (groundGunHitCount == 3)
        {
            if (posCheck > 0)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitGroundFront);
            }
            else
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitGroundBack);
            }
            groundGunHitCount = 0;
        }
    }

    public void BeHitAir()
    {
        if (posCheck > 0)
        {
            if (airSwordHitCount + airGunHitCount == 1)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitAirFront);
            }
            else if (airSwordHitCount + airGunHitCount == 3 && !isContinueAnim)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.DownAirFront1);
                StartCoroutine(DownInvincible());
            }
        }
        else
        {
            if (airSwordHitCount + airGunHitCount == 1)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.HitAirBack);
            }
            else if (airSwordHitCount + airGunHitCount == 3 && !isContinueAnim)
            {
                PlayerAnimation.Instance.HitAnimation(HitState.DownAirBack1);
                StartCoroutine(DownInvincible());
            }
        }
    }

    IEnumerator DownInvincible()
    {
        isContinueAnim = true;
        IsInvincible = true;
        yield return new WaitForSeconds(downTime);
        IsInvincible = false;
        isContinueAnim = false;
        groundGunHitCount = groundSwordHitCount = airGunHitCount = airSwordHitCount = 0;
    }
}
