using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFunnelControl : MonoBehaviour
{
    public BossFunnel[] funnels;

    public int initialHealth = 2;
    public float recovertyTimePer1Health = 3f;

    public float minDis = 40f, maxDis = 60f;

    public float moveSpeed = 4f;
    public float rotateSpeed = 5f;
    [Tooltip("이동 주기")]
    public float funnelCycle = 1f;
    public float yInterval = 100f;
    public float groundY = 15f, skyY = 500f;

    [Tooltip("한회당 공격 횟수")]
    public int attackCount = 2;
    [Tooltip("공격 주기")]
    public float attackCycle;
    [Tooltip("판넬 빔 사라지는 속도")]
    public float attackSpeed = 5f;

    public float sallyDelay = 5f;

    public int damage = 2;

    private bool readyToSally;

    private void Awake()
    {
        InitialVariable();
    }

    private void Start()
    {
        Sally(false);
    }

    private void InitialVariable()
    {
        foreach (BossFunnel funnel in funnels)
        {
            funnel.InitialVariable(initialHealth, recovertyTimePer1Health, maxDis, minDis,
                moveSpeed, rotateSpeed, funnelCycle, yInterval,
                groundY, skyY, attackCount, attackCycle, attackSpeed, damage);
        }
    }

    public void Sally(bool _enable)
    {
        if (_enable)
        {
            if (!readyToSally)
            {
                StartCoroutine(SallyCoroutine());
            }
        }
        else
        {
            StopAllCoroutines();
        }
    }

    private IEnumerator SallyCoroutine()
    {
        readyToSally = true;

        foreach (BossFunnel funnel in funnels)
        {
            if (funnel.GetIsAlive())
            {
                funnel.Action(true);

                yield return new WaitForSeconds(sallyDelay);
            }
        }

        readyToSally = false;
    }
}
