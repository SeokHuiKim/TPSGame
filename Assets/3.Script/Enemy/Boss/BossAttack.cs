using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public enum BossWeapon
{
    Melee,
    Range
}

public class BossAttack : BossAction
{
    public GameObject rangeWeapon, meleeWeapon;
    public BoxCollider meleeCollider;

    public float rotateSpeed = 10f;

    public float rushMag = 3f;

    public float rangeAttackDelay, meleeAttackDelay;

    [Range(0f, 1f)]
    public float rangeAttackTiming = 0.5f;

    private MeleeWeaponTrail[] trails;

    private void OnDisable()
    {
        StopAllCoroutines();
    }

    protected override void AwakeReset()
    {
        SetWeapon(BossWeapon.Range);
        meleeCollider.enabled = false;
    }

    private void Start() {
        TrailOn(false);
    }

    private void TrailOn(bool _enable)
    {
        trails = meleeCollider.GetComponentsInChildren<MeleeWeaponTrail>();
        foreach (MeleeWeaponTrail trail in trails)
        {
            trail.Emit = _enable;
        }
    }

    public void SetWeapon(BossWeapon _weapon)
    {
        if (_weapon.Equals(BossWeapon.Range))
        {
            trails = meleeCollider.GetComponentsInChildren<MeleeWeaponTrail>();
            foreach (MeleeWeaponTrail trail in trails)
            {
                trail.DestroyTrail();
            }
            rangeWeapon.SetActive(true);
            meleeWeapon.SetActive(false);
        }
        else
        {
            rangeWeapon.SetActive(false);
            meleeWeapon.SetActive(true);
        }
    }

    // 공격 함수는 애니메이션에서 적용시켜야함
    public override void Action(bool _isRangeAttack)
    {
        if (_isRangeAttack)
        {
            SetWeapon(BossWeapon.Range);
            StartCoroutine(LookTarget());
            control.UseArmor();
            StartCoroutine(WaitForEnd(_isRangeAttack));
        }
        else
        {
            TrailOn(true);
            meleeCollider.enabled = true;

            SetWeapon(BossWeapon.Melee);
            control.NavControl(true);
            control.SetNav(control.player.position);
            control.NavSpeed(true, rushMag);
            anim.Play(AnimState.MeleeAttack);
            StartCoroutine(WaitForEnd(_isRangeAttack));
        }
    }

    private IEnumerator WaitForEnd(bool _isRangeAttack)
    {
        float delay = 0f;
        float baseline = 0f;
        bool once = false;
        if (_isRangeAttack)
        {
            baseline = rangeAttackDelay;
        }
        else
        {
            baseline = meleeAttackDelay;
        }
        while (delay < baseline)
        {
            if (delay > baseline * rangeAttackTiming && _isRangeAttack && !once)
            {
                once = true;
                anim.Play(AnimState.RangeAttack);
            }

            delay += Time.fixedDeltaTime;
            yield return new WaitForFixedUpdate();
        }

        bool check = true;
        while (check)
        {
            if (_isRangeAttack)
            {
                check = anim.GetStateInfo(AnimState.RangeAttack);
            }
            else
            {
                check = anim.GetStateInfo(AnimState.MeleeAttack);
            }

            yield return new WaitForFixedUpdate();
        }

        if (!_isRangeAttack)
        {
            TrailOn(false);
            meleeCollider.enabled = false;
        }

        control.NavControl(false);
        control.ActionControl(BossActions.Attack, false);
    }

    private IEnumerator LookTarget()
    {
        while (enabled)
        {
            Lect.Rotate(transform, control.player.position, rotateSpeed);
            transform.rotation = new Quaternion(0, transform.rotation.y, 0, transform.rotation.w);
            yield return new WaitForFixedUpdate();
        }
    }
}
