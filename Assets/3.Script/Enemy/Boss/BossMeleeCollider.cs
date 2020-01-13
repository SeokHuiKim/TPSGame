using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMeleeCollider : MonoBehaviour
{
    public float hitDistance = 12f;

    public float damageInterval = 0.2f;
    public PlayerHUD player;
    public PlayerHit playerHit;

    public bool damageFlag;

    public Transform playerTransform;

    private void Start() { }

    private void Awake()
    {
        playerTransform = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void OnEnable()
    {
        damageFlag = false;
    }

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    private IEnumerator TimeSpan()
    {
        float time = 0f;
        while (time < damageInterval)
        {
            time += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        damageFlag = false;
    }

    public void GetTarget(int _damage, Transform _boss)
    {
        if(Lect.DistanceCheck(transform, playerTransform, hitDistance))
        {
            return;
        }

        if (!damageFlag)
        {
            // 플레이어에게 대미지
            if (!playerHit.IsInvincible)
            {
                playerHit.FrontBackCheck(playerTransform, _boss);
                playerHit.SwordHit(1);
                if (Flat.Instance.GetGround()) playerHit.BeHitSword();
                else playerHit.BeHitAir();
                player.DecreaseHp(_damage);
            }

            if (!damageInterval.Equals(0) && !damageFlag)
            {
                damageFlag = true;
                StartCoroutine(TimeSpan());
            }
        }
    }
}
