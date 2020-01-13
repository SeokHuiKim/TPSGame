using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationSound : StateMachineBehaviour
{
    private int index, index1;

    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        index = index1 = 0;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Booster_End") || stateInfo.IsName("BackBooster_End") || stateInfo.IsName("RightBooster_End") || stateInfo.IsName("LeftBooster_End") || stateInfo.IsName("Jump_End"))
        {
            if (index == 0)
            {
                //sound.LandingSound(player);
                index++;
            }
        }
        else if ((stateInfo.IsName("Down_Front") || stateInfo.IsName("Down_Back")) && stateInfo.normalizedTime > 0.5f)
        {
            if (index1 == 0)
            {
                //sound.WakeUpSound(player);
                index1++;
            }
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        index = index1 = 0;
    }

}
