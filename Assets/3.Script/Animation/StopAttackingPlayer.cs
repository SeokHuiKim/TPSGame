using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StopAttackingPlayer : StateMachineBehaviour
{
    private GameObject player;
    private Rigidbody rigid;


    public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateEnter(animator, stateInfo, layerIndex);
        player = GameObject.FindGameObjectWithTag("Player");
        rigid = player.GetComponent<Rigidbody>();
    }

    public override void OnStateUpdate(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateUpdate(animator, stateInfo, layerIndex);
        player.GetComponent<PlayerCtrl>().SetStop(true);
        rigid.useGravity = false;
        rigid.velocity = Vector3.zero;
    }

    public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
    {
        base.OnStateExit(animator, stateInfo, layerIndex);
        player.GetComponent<PlayerCtrl>().SetStop(false);
        rigid.useGravity = true;
    }
}

