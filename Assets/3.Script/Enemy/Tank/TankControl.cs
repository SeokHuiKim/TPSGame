using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankMove)), RequireComponent(typeof(TankAttack))]
public class TankControl : EnemyControl
{
    private void Awake()
    {
        ResetState(GetComponent<TankAttack>(), GetComponent<TankMove>());
    }

    public override void StartSet()
    {
        foreach (EnemyAction s in states.Values)
        {
            s.enabled = true;
            s.EnableState();
        }
    }
}
