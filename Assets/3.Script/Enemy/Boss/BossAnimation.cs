using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AnimState
{
    Walk,
    WalkStop,
    Boost,
    BoostStop,
    MeleeAttack,
    RangeAttack,
    Damage,
    Down, 
    Sally,
    Die
}

public class BossAnimation : MonoBehaviour
{
    private Animator anim;

    private void Awake()
    {
        anim = GetComponentInChildren<Animator>();
    }

    private void Start() { }

    public void Play(AnimState state)
    {
        switch (state)
        {
            case AnimState.Walk:
                anim.SetBool("walk", true);
                break;
            case AnimState.WalkStop:
                anim.SetBool("walk", false);
                break;
            case AnimState.Boost:
                anim.SetBool("boost", true);
                break;
            case AnimState.BoostStop:
                anim.SetBool("boost", false);
                break;
            case AnimState.MeleeAttack:
                anim.SetTrigger("meleeAttack");
                break;
            case AnimState.RangeAttack:
                if (Random.Range(0, 2).Equals(0))
                {
                    anim.SetTrigger("rangeAttack");
                }
                else
                {
                    anim.SetTrigger("rangeAttack2");
                }
                break;
            case AnimState.Damage:
                anim.SetTrigger("damage");
                break;
            case AnimState.Down:
                anim.SetTrigger("down");
                break;
            case AnimState.Sally:
                anim.SetTrigger("sally");
                break;
            case AnimState.Die:
                anim.SetBool("die", true);
                break;
        }
    }

    public bool GetStateInfo(AnimState state)
    {
        string _name = string.Empty;

        switch (state)
        {
            case AnimState.Walk:
                _name = "Walk";
                break;
            case AnimState.Boost:
                _name = "Boost";
                break;
            case AnimState.MeleeAttack:
                _name = "MeleeAttack";
                break;
            case AnimState.RangeAttack:
                _name = "RangeAttack";
                break;
        }

        return anim.GetCurrentAnimatorStateInfo(0).IsName(_name);
    }
}
