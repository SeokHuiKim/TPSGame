using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHUD : MonoBehaviour
{
    // 체력
    [SerializeField]
    private int currentHp;
    public int hp;

    // 실드
    [SerializeField]
    private int currentDp;
    public int dp;

    // 스태미너
    private int currentSp;
    public int sp;

    // 맞는 시간
    [HideInInspector] public bool isHeat;
    private bool isHitTime;

    // 쉴드, 스태미너 증가량
    public int dpIncreaseSpeed;
    public int spIncreaseSpeed;

    // 쉴드, 스태미너 재회복 딜레이
    public int spRechargeTime;
    private int currentSpRechargeTime;
    public int dpRechargeTime;
    private int currentDpRechargeTime;

    // 쉴드, 스태미너 감소 여부
    private bool dpUsed, spUsed;

    public GameSetting setting;
    public Image[] images_Gauge;
    public GameObject spGauge;

    public HealthWarning warning;

    public DisappearHUD spear;

    private void Start()
    {
        currentHp = hp;
        currentDp = dp;
        currentSp = sp;
    }

    private void Update()
    {
        GaugeUpdate();
        HitCheck();
        DPRecover();
        SPRecover();
    }

    private void HpLow(bool _enable)
    {
        warning.Action(_enable);
    }

    private void GaugeUpdate()
    {
        images_Gauge[0].fillAmount = (float)currentDp / dp;
        images_Gauge[1].fillAmount = (float)currentSp / sp;
    }

    public void DecreaseSp(int _count)
    {
        spear.Appear();

        spUsed = false;
        if (currentSp > _count)
            currentSp -= _count;
        else
            currentSp = 0;
    }

    private void SPRecover()
    {
        StartCoroutine(SpCheck());
        if (spUsed && currentSp < sp && Flat.Instance.GetGround() && !Input.GetKey(KeyCode.LeftShift))
            currentSp += spIncreaseSpeed;
        if(currentSp >= sp)
        {
            currentSp = sp;
        }
    }

    public void DecreaseHp(int _value)
    {
        if (currentDp > _value)
        {
            DecreaseDp(_value);
            return;
        }
        else
        {
            _value -= currentDp;
            DecreaseDp(_value);
        }

        HpLow(true);
        currentHp -= _value;
        if (currentHp < 0)
            StartCoroutine(setting.GameOver());
    }

    public void DecreaseDp(int _count)
    {
        isHitTime = true;
        if (currentDp - _count > 0)
        {
            currentDp -= _count;
        }
        else currentDp = 0;
    }

    private void DPRecover()
    {
        if (dpUsed && currentDp < dp)
        {
            //hp 회복
            if (currentHp <= hp) currentHp += dpIncreaseSpeed;
            else currentHp = hp;

            if (currentDp > dp * 0.25f)
            {
                HpLow(false);
            }

            currentDp += dpIncreaseSpeed;
            currentDpRechargeTime = 0;
            dpUsed = false;
        }
        else if (currentDp == dp)
        {
            isHitTime = false;
        }
    }

    private void HitCheck()
    {
        if (isHitTime)
        {
            if (currentDpRechargeTime < dpRechargeTime)
            {
                currentDpRechargeTime++;
                if (currentDpRechargeTime > 0 && currentDpRechargeTime < dpRechargeTime)
                {
                    if (isHeat)
                    {
                        currentDpRechargeTime = 0;
                        isHeat = false;
                    }
                }
            }
            else
            {
                isHeat = false;
                dpUsed = true;
            }
        }
    }

    public int CurrentSp()
    {
        return currentSp;
    }

    private IEnumerator SpCheck()
    {
        int check1, check2;
        while (true)
        {
            check1 = currentSp;
            yield return new WaitForSeconds(1f);
            check2 = currentSp;
            if(check1 == check2)
            {
                spUsed = true;
            }
            break;
        }
    }
}