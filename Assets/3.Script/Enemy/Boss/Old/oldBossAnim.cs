//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class BossAnim : EnemyAnim
//{
//    private Animator anim;
    
//    private bool walk;

//    private void Awake()
//    {
//        anim = GetComponentInChildren<Animator>();

//        Play(AnimState.Boost);
//    }

//    public override void Play(AnimState state)
//    {
//        switch (state)
//        {
//            case AnimState.Walk:
//                anim.SetBool("boost", false);
//                break;
//            case AnimState.Boost:
//                anim.SetBool("boost", true);
//                break;
//            case AnimState.MeleeAttack:
//                anim.SetTrigger("meleeAttack");
//                break;
//            case AnimState.RangeAttack:
//                anim.SetTrigger("rangeAttack");
//                break;
//        }
//    }

//    public override bool GetStateInfo(AnimState state)
//    {
//        string _name = string.Empty;

//        switch (state)
//        {
//            case AnimState.Walk:
//                _name = "Walk";
//                break;
//            case AnimState.Boost:
//                _name = "Boost";
//                break;
//            case AnimState.MeleeAttack:
//                _name = "MeleeAttack";
//                break;
//            case AnimState.RangeAttack:
//                _name = "RangeAttack";
//                break;
//        }

//        return anim.GetCurrentAnimatorStateInfo(0).IsName(_name);
//    }
//}
