using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class BossAnimFunction : MonoBehaviour
{
    public BossMove move;
    public BossMeleeCollider melee;

    [HideInInspector]
    public Vector3 playerCenter;
    public Transform player;
    public Transform boss;
    public Transform muzzle;
    public PlayerHit playerHit;
    public LineRenderer rangeLine;

    public PlayerHUD playerHUD;
    public int rangeDamage, meleeDamage;
    
    private RaycastHit hit;
    private NavMeshAgent nav;
    private float spd, acc;
    private BossControl control;

    private void Start() {
        rangeLine.enabled = false;
        nav = boss.GetComponent<NavMeshAgent>();
        control = boss.GetComponent<BossControl>();
    }

    public void Down()
    {
        spd = nav.speed;
        acc = nav.acceleration;

        nav.speed = 0f;
        nav.acceleration = 0f;
    }
    public void Wake()
    {
        nav.speed = spd;
        nav.acceleration = acc;
    }

    public void DecreaseBoostValue(float _value)
    {
        move.DecreaseBoostValue(_value);
    }
    public void IncreaseBoostValue(float _value)
    {
        move.IncreaseBoostValue(_value);
    }
    public void MeleeAttack()
    {
        control.SetNav(control.player.position);
        melee.GetTarget(meleeDamage, boss);
    }

    public void RailGunEffect()
    {
        if (!playerHit.IsInvincible)
        {
            Vector3 origin = muzzle.position;
            Vector3 dest = boss.transform.forward;
            //playerCenter = player.GetComponent<BoxCollider>().bounds.center;
            //Vector3 dir = (playerCenter - muzzle.position).normalized;

            if (Physics.Raycast(origin, dest, out hit, 200f, 1 << 9 | 1 << 13, QueryTriggerInteraction.Ignore))
            {
                StartCoroutine(RangeLine(origin, hit.point));

                GameObject effect1 = ObjectPool.Instance.PopFromPool("RailBoom");
                //GameObject effect2 = ObjectPool.Instance.PopFromPool("GatlingBoom");
                effect1.transform.position = hit.point;
                //effect2.transform.position = hit.point;
                effect1.SetActive(true);
                //effect2.SetActive(true);

                if (hit.transform.CompareTag("Player"))
                {
                    playerHUD.DecreaseHp(rangeDamage);
                    playerHit.FrontBackCheck(player, boss);
                    playerHit.GunHit(1);

                    if (Flat.Instance.GetGround()) playerHit.BeHitGun();
                    else playerHit.BeHitAir();
                }
            }
            else
            {
                StartCoroutine(RangeLine(origin, dest * 200f));
            }
        }
    }

    private IEnumerator RangeLine(Vector3 _origin, Vector3 _dest)
    {
        rangeLine.enabled = true;
        rangeLine.SetPosition(0, _origin);
        rangeLine.SetPosition(1, _dest);

        float val2 = 1f;
        while (val2 > 0f)
        {
            val2 -= Time.fixedDeltaTime * 5;
            rangeLine.startWidth = val2;
            rangeLine.endWidth = val2;

            yield return new WaitForFixedUpdate();
        }
        rangeLine.enabled = false;
    }
}
