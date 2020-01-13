using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAniamtion : StateMachineBehaviour
{
    private GameObject player;
    private GameObject sword;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        player = GameObject.FindGameObjectWithTag("Player");
        sword = GameObject.Find("B_saber");
        if (stateInfo.IsName("Sword_First"))
        {
            GameObject.Find("Modelling").GetComponent<WeaponManager>().SetChange(true);
            GameObject.Find("GUI_WeaponSlot").GetComponent<WeaponSlot>().SetSlotChange(true);
            GameObject.Find("Modelling").GetComponent<CloseWeaponController>().SetSwing(true);
        }
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Sword_Thrid"))
        {
            player.transform.Translate(Vector3.forward);
        }
        sword.GetComponent<MeleeWeaponTrail>().Emit = true;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        sword.GetComponent<MeleeWeaponTrail>().Emit = false;
        if (stateInfo.IsName("Sword_Thrid"))
        {
            GameObject.Find("Modelling").GetComponent<CloseWeaponController>().SetSwing(false);
            GameObject.Find("Modelling").GetComponent<WeaponManager>().SetChange(false);
            GameObject.Find("GUI_WeaponSlot").GetComponent<WeaponSlot>().SetSlotChange(false);
        }
    }
}
