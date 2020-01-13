using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponChange : StateMachineBehaviour
{
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        GameObject.Find("Modelling").GetComponent<WeaponManager>().SetChange(true);
        GameObject.Find("GUI_WeaponSlot").GetComponent<WeaponSlot>().SetSlotChange(true);
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);

    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        GameObject.Find("Modelling").GetComponent<WeaponManager>().SetChange(false);
        GameObject.Find("GUI_WeaponSlot").GetComponent<WeaponSlot>().SetSlotChange(false);
    }
}
