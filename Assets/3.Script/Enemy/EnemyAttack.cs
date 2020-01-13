using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAttack : EnemyAction
{
    public GameObject bullet;
    public Transform[] muzzles;

    [Tooltip("몇번공격")]
    public float attackCount;
    [Tooltip("공격주기")]
    public float attackRate;
    [Tooltip("공격시간")]
    public float attackTime;
    [Tooltip("이동공격같이하는지")]
    public bool isAttackWithMove;
    [Tooltip("근접공격 최소거리")]
    public float meleeAttackDistance;

    [Tooltip("애니메이션이 있는지")]
    public bool isAnimOn;

    private bool attackReady;
    private bool moveNow;

    private void Start()
    {
        StartCoroutine(AttackRate());
    }

    public virtual void Attack(bool isMelee)
    {
        for (int i = 0; i < attackCount; i++)
            foreach (Transform muzzle in muzzles)
                Instantiate(bullet, muzzle);
    }

    private IEnumerator AttackRate()
    {
        float rate = 0f, time = attackTime;

        while (true)
        {
            while (attackRate.Equals(0f)) yield return new WaitForSeconds(1f);

            if (!moveNow)
            {
                if (time > attackTime)
                {
                    moveNow = true;
                    control.WaitMove(false);
                }
                else if (time < attackTime) time += Time.deltaTime;
            }

            if (rate > attackRate)
            {
                if (!attackReady)
                    yield return new WaitForFixedUpdate();
                attackReady = false;
                rate = 0f;

                float dis = (transform.position - control.player.position).sqrMagnitude;
                if (dis > Mathf.Pow(meleeAttackDistance, 2f)) Attack(true);
                else Attack(false);

                if (!isAttackWithMove)
                {
                    time = 0f;
                    moveNow = false;
                    control.WaitMove(true);
                }
            }
            else rate += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }
    }
}
