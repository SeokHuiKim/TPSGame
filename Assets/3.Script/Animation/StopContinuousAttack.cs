﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopContinuousAttack : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        //GameObject.Find("Modelling").GetComponent<CloseWeaponController>().isSwing = true;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        //GameObject.Find("Modelling").GetComponent<CloseWeaponController>().isSwing = false;
    }
}
