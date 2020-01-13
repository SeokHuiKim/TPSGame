using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationMove : StateMachineBehaviour
{
    private GameObject player;
    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        player = GameObject.Find("Player");
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        if (stateInfo.IsName("Down_Ground_Front") && (stateInfo.normalizedTime < 0.2f))
        {
            player.transform.Translate(new Vector3(0, 0, -3f) * Time.fixedDeltaTime);
        }
        if (stateInfo.IsName("Down_Ground_Back") && ((stateInfo.normalizedTime > 0.12f && stateInfo.normalizedTime < 0.3f)))
        {
            player.transform.Translate(new Vector3(0, 0, 3f) * Time.fixedDeltaTime);
        }
        if (stateInfo.IsName("Down_Ground_Back") && ((stateInfo.normalizedTime > 0.5f && stateInfo.normalizedTime < 0.8f)))
        {
            player.transform.Translate(new Vector3(0, 0, 3f) * Time.fixedDeltaTime);
        }
        if (stateInfo.IsName("Hit_Air_Front"))
        {
            player.transform.Translate(new Vector3(0, 0, -3f) * Time.fixedDeltaTime);
        }
        if (stateInfo.IsName("Hit_Air_Back"))
        {
            player.transform.Translate(new Vector3(0, 0, 3f) * Time.fixedDeltaTime);
        }
        if (stateInfo.IsName("Down_Air_Front_First") && stateInfo.normalizedTime > 0.3f)
        {
            player.GetComponent<PlayerCtrl>().Fall(true);
            if (Flat.Instance.GetGround())
            {
                Debug.Log("?");
                PlayerAnimation.Instance.HitAnimation(HitState.DownAirFront2);
            }
        }
        if (stateInfo.IsName("Down_Air_Back_First") && stateInfo.normalizedTime > 0.3f)
        {
            player.GetComponent<PlayerCtrl>().Fall(true);
            if (Flat.Instance.GetGround())
            {
                Debug.Log("?");
                PlayerAnimation.Instance.HitAnimation(HitState.DownAirBack2);
            }
        }
        if (stateInfo.IsName("Down_Air_Back_Second") && ((stateInfo.normalizedTime > 0.5f && stateInfo.normalizedTime < 0.8f)))
        {
            player.transform.Translate(new Vector3(0, 0, 3f) * Time.fixedDeltaTime);
        }
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
    }
}
