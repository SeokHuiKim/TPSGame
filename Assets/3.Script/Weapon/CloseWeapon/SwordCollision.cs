using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordCollision : MonoBehaviour
{
    public BossControl boss;

    public GameObject bossTarget, bossFunnelTarget, enemyTarget;

    private void OnTriggerEnter(Collider col)
    {
        if (col.transform.CompareTag("Boss"))
        {
            boss.Damage(25, false);
        }
        else if (col.transform.CompareTag("BossFunnel"))
        {
            bossFunnelTarget = col.transform.gameObject;
        }
        else if (col.transform.CompareTag("Enemy"))
        {
            enemyTarget = col.transform.gameObject;
        }
    }

    public void Target()
    {
        if(bossTarget != null)
        {
            boss.Damage(25, false);
        }
        else if(bossFunnelTarget != null)
        {
            bossFunnelTarget.GetComponent<BossFunnel>().Damage(20);
        }
        else if (enemyTarget != null)
        {
            enemyTarget.GetComponent<EnemyControl>().Damage(20);
        }
    }

    public void TargetReset()
    {
        bossTarget = bossFunnelTarget = enemyTarget = null;
    }
}
