using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(JetMove)), RequireComponent(typeof(JetAttack))]
public class JetControl : EnemyControl
{
    private void Awake()
    {
        ResetState(GetComponent<JetAttack>(), GetComponent<JetMove>());
    }

    public override void StartSet()
    {
        foreach (EnemyAction s in states.Values)
        {
            s.enabled = true;
            s.EnableState();
        }
    }

    public override void SetState(EnemyState state)
    {
        states[state].enabled = true;
        states[state].EnableState();
    }
}
