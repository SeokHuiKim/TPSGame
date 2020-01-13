using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankControl))]
public class TankAttack : EnemyAttack
{
    private Animator anim;

    public override void AwakeReset()
    {
        base.AwakeReset();
        //anim = GetComponent<Animator>();
    }
}
