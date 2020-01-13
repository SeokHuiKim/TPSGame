using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationReset : StateMachineBehaviour
{
    private bool isActive;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        isActive = false;
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Rail_Idle"))
        {
            isActive = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.Space))
                || stateInfo.normalizedTime > 1.0f ? true : false;
        }
        else if (stateInfo.IsName("Rail_FW") || stateInfo.IsName("Rail_FB"))
        {
            isActive = (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || stateInfo.normalizedTime > 1.0f ? true : false;
        }
        else if (stateInfo.IsName("Rail_BW") || stateInfo.IsName("Rail_BB"))
        {
            isActive = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) || stateInfo.normalizedTime > 1.0f ? true : false;
        }
        else if (stateInfo.IsName("Rail_LW") || stateInfo.IsName("Rail_LB"))
        {
            isActive = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.D)) || stateInfo.normalizedTime > 1.0f ? true : false;
        }
        else if (stateInfo.IsName("Rail_RW") || stateInfo.IsName("Rail_RB"))
        {
            isActive = (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.Space)) || stateInfo.normalizedTime > 1.0f ? true : false;
        }
        PlayerAnimation.Instance.Idle(isActive);
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        isActive = false;
        PlayerAnimation.Instance.Idle(isActive);
    }
}
