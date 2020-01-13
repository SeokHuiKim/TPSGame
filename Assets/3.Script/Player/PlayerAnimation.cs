using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum HitState
{
    HitGroundFront, HitGroundBack, HitAirFront, HitAirBack,
    DownGroundFront, DownGroundBack, DownAirFront1, DownAirFront2, DownAirBack1, DownAirBack2
}

public class PlayerAnimation : Singleton<PlayerAnimation>
{
    // 상태변수
    private bool isWalk, isBackWalk, isLeftWalk, isRightWalk;
    private bool isBooster, isBackBooster, isLeftBooster, isRightBooster;

    public Animator anim;
    public PlayerCtrl playerCtrl;
    public PlayerHUD playerHUD;

    private void Update()
    {
        Walk_Animation();
        Booster_Animation();
        //Jump_Animation();
    }

    public void SwordAttackAnimation()
    {
        anim.SetTrigger("Sword_Attack");
    }

    public void RailGun_Attack_Animation()
    {
        if (Flat.Instance.GetGround())
        {
            if (Input.GetKey(KeyCode.W) && !Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_FW"); }
            else if (Input.GetKey(KeyCode.S) && !Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_BW"); }
            else if (Input.GetKey(KeyCode.A) && !Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_LW"); }
            else if (Input.GetKey(KeyCode.D) && !Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_RW"); }
            else if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_FB"); }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_BB"); }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_LB"); }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_RB"); }
            else { anim.SetTrigger("Rail_Idle"); }
        }
        else if (!Flat.Instance.GetGround())
        {
            if (Input.GetKey(KeyCode.W) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_FB"); }
            else if (Input.GetKey(KeyCode.S) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_BB"); }
            else if (Input.GetKey(KeyCode.A) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_LB"); }
            else if (Input.GetKey(KeyCode.D) && Input.GetKey(KeyCode.LeftShift)) { anim.SetTrigger("Rail_RB"); }
            else { anim.SetTrigger("Rail_Jump"); }
        }
    }

    public void Missile_Attack_Animation()
    {
        anim.SetTrigger("Missile_Idle");
    }

    public void Beam_Attack_Animation()
    {
        if (!Flat.Instance.GetGround()) { anim.SetTrigger("Beam_Jump"); }
        else { anim.SetTrigger("Beam_Idle"); }
    }

    public void Walk_Animation()
    {
        isWalk = Input.GetKey(KeyCode.W) ? true : false;
        isBackWalk = Input.GetKey(KeyCode.S) ? true : false;
        isLeftWalk = Input.GetKey(KeyCode.A) ? true : false;
        isRightWalk = Input.GetKey(KeyCode.D) ? true : false;
        if (Flat.Instance.GetGround())
        {
            anim.SetBool("FW", isWalk);
            anim.SetBool("BW", isBackWalk);
            anim.SetBool("LW", isLeftWalk);
            anim.SetBool("RW", isRightWalk);
        }
    }

    public void Booster_Animation()
    {
        isBooster = (Input.GetKey(KeyCode.LeftShift) && isWalk && playerHUD.CurrentSp() > 0) ? true : false;
        isBackBooster = (Input.GetKey(KeyCode.LeftShift) && isBackWalk && playerHUD.CurrentSp() > 0) ? true : false;
        isLeftBooster = (Input.GetKey(KeyCode.LeftShift) && isLeftWalk && playerHUD.CurrentSp() > 0) ? true : false;
        isRightBooster = (Input.GetKey(KeyCode.LeftShift) && isRightWalk && playerHUD.CurrentSp() > 0) ? true : false;
        anim.SetBool("FB", isBooster);
        anim.SetBool("BB", isBackBooster);
        anim.SetBool("LB", isLeftBooster);
        anim.SetBool("RB", isRightBooster);
    }

    public void Jump_Animation()
    {
        if (Input.GetKey(KeyCode.Space) && playerHUD.CurrentSp() > 0)
        {
            anim.SetBool("Jump", true);
        }
        else if (!Input.GetKey(KeyCode.Space) && Flat.Instance.GetGround())
        {
            anim.SetBool("Jump", false);
        }
    }

    public void Idle(bool isActive)
    {
        anim.SetBool("Idle", isActive);
    }

    public void HitAnimation(HitState hit)
    {
        switch (hit)
        {
            case HitState.HitGroundFront:
                anim.SetTrigger("Hit_Ground_Front");
                break;
            case HitState.HitGroundBack:
                anim.SetTrigger("Hit_Ground_Back");
                break;
            case HitState.DownGroundFront:
                anim.SetTrigger("Down_Ground_Front");
                break;
            case HitState.DownGroundBack:
                anim.SetTrigger("Down_Ground_Back");
                break;
            case HitState.HitAirFront:
                anim.SetTrigger("Hit_Air_Front");
                break;
            case HitState.DownAirFront1:
                anim.SetTrigger("Down_Air_Front_First");
                break;
            case HitState.DownAirFront2:
                if (Flat.Instance.GetGround())
                    anim.SetTrigger("Down_Air_Front_Second"); Debug.Log("?2");
                break;
            case HitState.HitAirBack:
                anim.SetTrigger("Hit_Air_Back");
                break;
            case HitState.DownAirBack1:
                anim.SetTrigger("Down_Air_Back_First");
                break;
            case HitState.DownAirBack2:
                if (Flat.Instance.GetGround())
                    anim.SetTrigger("Down_Air_Back_Second"); Debug.Log("?2");
                break;
        }
    }
}
