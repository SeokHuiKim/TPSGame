using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.UI;

public enum BossActions
{
    Finish,
    Attack,
    Move
}

public class BossControl : MonoBehaviour
{
    public GameSetting setting;

    public Image[] healthImages;

    public float damagedAfterReactionTime = 0.5f;

    public int maxArmor = 10, maxHealth = 100;

    public float rushDistance = 50f;

    public int rechargeArmorTurn = 1;

    [Tooltip("다운 카운트 추가를 위한 최소 대미지")]
    public int minDamageForDownCount = 10;

    [Tooltip("몇초 안에 다시 때려야 다운 카운트가 적용되는지")]
    public float downCountDelay = 1f;

    [Tooltip("다운 카운트가 몇번 적용되야 다운이 되는지")]
    public int downCount = 3;

    [SerializeField]
    private int currentArmor, currentHealth, currentRechargeArmorTurn;

    private bool moveFinish, attackFinish;

    private int damageCount;

    private Dictionary<BossActions, BossAction> action =
        new Dictionary<BossActions, BossAction>();

    [HideInInspector]
    public Transform player;
    [HideInInspector]
    public NavMeshAgent nav;

    private float moveSpeed, acceleration;

    private BossAnimation anim;
    private BossFunnelControl funnel;

    private bool invincible, meleeDelay, sally;
    private float allDisableTime;

    private void Awake()
    {
        action.Add(BossActions.Move, GetComponent<BossMove>());
        action.Add(BossActions.Attack, GetComponent<BossAttack>());
        player = GameObject.FindGameObjectWithTag("Player").transform;
        nav = GetComponent<NavMeshAgent>();
        moveSpeed = nav.speed;
        acceleration = nav.acceleration;
        anim = GetComponent<BossAnimation>();
        funnel = GetComponent<BossFunnelControl>();
    }

    private void FixedUpdate()
    {
        transform.position = new Vector3(
            transform.position.x, 1.6f, transform.position.z);

        CoroutineCheck();
    }

    private void CoroutineCheck()
    {
        if(!action[BossActions.Attack].enabled &&
            !action[BossActions.Move].enabled)
        {
            allDisableTime += Time.fixedDeltaTime;
        }

        if(allDisableTime >= 1f)
        {
            allDisableTime = 0f;
            anim.Play(AnimState.WalkStop);
            anim.Play(AnimState.BoostStop);
            Action();
        }
    }

    public bool GetSally()
    {
        return sally;
    }

    private void Start()
    {
        nav.enabled = false;
        AllActionOff();

        StartSet();
    }

    private void StartSet()
    {
        currentArmor = maxArmor;
        currentHealth = maxHealth;
        currentRechargeArmorTurn = rechargeArmorTurn;
        Action();
    }

    public void Poweroverwhelming(bool _enable)
    {
        invincible = _enable;
    }

    public void Damage(int _value, bool _down)
    {
        if (_down)
        {
            damageCount = 0;
            Poweroverwhelming(true);
            DamageAnim(false);
        }
        else
        {
            if (damageCount < downCount)
            {
                if (_value >= minDamageForDownCount)
                {
                    //다운 중 대미지 카운트 올라가지 않음
                    if (!invincible)
                    {
                        DamageAnim(true);
                        StartCoroutine(DamageCount());
                        damageCount++;
                    }
                }
            }
            else
            {
                damageCount = 0;
                Poweroverwhelming(true);
                DamageAnim(false);
            }
        }

        currentHealth -= _value;
        healthImages[0].fillAmount = (float)currentHealth / (float)maxHealth;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    private void StopAll()
    {
        StopAllCoroutines();
        ActionControl(BossActions.Move, false);
        ActionControl(BossActions.Attack, false);
        nav.enabled = false;
        nav.enabled = true;
    }

    private void DamageAnim(bool _hitOrDown)
    {
        StopAll();

        if (_hitOrDown)
        {
            anim.Play(AnimState.Damage); 
            StartCoroutine(ReAction(false));
        }
        else
        {
            anim.Play(AnimState.Down);
            StartCoroutine(ReAction(true));
        }
    }

    private IEnumerator ReAction(bool x2)
    {
        float max = damagedAfterReactionTime;
        if (x2) max *= 2;
        float time = 0f;
        while (max >= time)
        {
            time += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        Poweroverwhelming(false);
        Action();
    }

    private IEnumerator DamageCount()
    {
        float damageCountTime = 0f;
        while (downCountDelay > damageCountTime)
        {
            damageCountTime += Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        damageCount = 0;
    }

    public void Action()
    {
        if (!sally)
        {
            if (currentHealth < maxHealth * 0.5f ||
                player.position.y > 30f)
            {
                sally = true;
                anim.Play(AnimState.Sally);
                funnel.Sally(true);
                currentArmor = 1000;
                moveSpeed *= 1.5f;
                acceleration *= 1.5f;
                minDamageForDownCount = (int)(minDamageForDownCount * 1.5f);
                damagedAfterReactionTime *= 0.8f;
            }
        }

        Pattern1();
    }

    private void Pattern1()
    {
        if (Lect.DistanceCheck(transform, player, rushDistance))
        {
            // 근접 딜레이 초기화
            meleeDelay = false;

            //조건 1. 플레이어와의 거리가 일정 이상일때
            if (currentArmor > 0)
            {
                // 조건1_1. 잔여 탄창 1 이상
                // 공격 활성화, 원거리 공격
                ActionControl(BossActions.Attack, true);
                action[BossActions.Attack].Action(true);

                // 가까운 위치로 이동, 이후 초기화
                StartCoroutine(WaitForNextAction(BossActions.Attack, BossActions.Move, true));
            }
            else
            {
                // 조건1_2. 잔여 탄창 0 이하
                // 이동 활성화, 가까운 위치로 이동, 재장전
                ActionControl(BossActions.Move, true);
                action[BossActions.Move].Action(true);
                ArmorRecharge();

                // 이후 초기화
                StartCoroutine(WaitForNextAction(BossActions.Move, BossActions.Finish, false));
            }
        }
        else
        {
            //조건 2. 플레이어와의 거리가 일정 미만일때
            if (meleeDelay)
            {
                // 조건2_1. 근접 딜레이가 있을 때
                // 이동 활성화, 먼 위치로 이동, 근접 딜레이 초기화
                ActionControl(BossActions.Move, true);
                action[BossActions.Move].Action(false);
                meleeDelay = false;

                // 먼 위치로 이동, 이후 초기화
                StartCoroutine(WaitForNextAction(BossActions.Move, BossActions.Move, false));
            }
            else
            {
                // 조건2_2. 근접 딜레이가 없을 때
                // 공격 활성화, 근접 공격, 근접 딜레이
                ActionControl(BossActions.Attack, true);
                action[BossActions.Attack].Action(false);
                meleeDelay = true;

                // 이후 초기화
                StartCoroutine(WaitForNextAction(BossActions.Attack, BossActions.Finish, false));
            }
        }
    }

    // 행동 관련
    public void ActionControl(BossActions _action, bool _enable)
    {
        action[_action].enabled = _enable;
    }
    public void AllActionOff()
    {
        // 행동 전부 비활성화
        foreach (BossAction act in action.Values)
        {
            act.Finish();
            act.enabled = false;
        }
    }

    // 행동이 종료될 때까지 기다림
    private IEnumerator WaitForNextAction(BossActions _waitingAction, BossActions _nextAction, bool _conditionOfNextAction)
    {
        if (_waitingAction != BossActions.Finish)
        {
            while (action[_waitingAction].enabled)
            {
                yield return new WaitForFixedUpdate();
            }
        }

        if (!_nextAction.Equals(BossActions.Finish))
        {
            ActionControl(_nextAction, true);
            action[_nextAction].Action(_conditionOfNextAction);
            StartCoroutine(WaitForNextAction(_nextAction, BossActions.Finish, false));
        }
        else
        {
            // 모든 행동이 끝나면 최초 조건으로 돌아감
            Action();
        }
    }

    // 네비 관련
    public bool IsNavMoving()
    {
        if (nav.velocity.x.Equals(0f) && nav.velocity.z.Equals(0f))
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    public void SetNav(Vector3 _destination)
    {
        try
        {
            nav.SetDestination(_destination);
        }
        catch(UnityException e)
        {
            nav.SetDestination(transform.position);
        }
    }
    public void NavControl(bool _enable)
    {
        nav.enabled = _enable;
    }
    public void NavSpeed(bool _boost, float _mag)
    {
        if (_boost)
        {
            nav.speed = moveSpeed * _mag;
            nav.acceleration = acceleration * _mag;
        }
        else
        {
            nav.speed = moveSpeed;
            nav.acceleration = acceleration;
        }
    }

    // 탄창 관련
    public int GetRemainArmor()
    {
        return currentArmor;
    }
    public void ArmorRecharge()
    {
        // 재장전 애니메이션?
        if (currentRechargeArmorTurn <= 0)
        {
            currentRechargeArmorTurn = rechargeArmorTurn;
            currentArmor = maxArmor;
        }
        else
        {
            currentRechargeArmorTurn--;
        }
    }
    public void UseArmor()
    {
        currentArmor--;
    }

    private void Die()
    {
        StopAll();
        anim.Play(AnimState.Die);
        setting.Win();
    }
}
