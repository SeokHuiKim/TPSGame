using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAction : MonoBehaviour
{
    protected BossControl control;
    protected BossAnimation anim;

    private void Awake()
    {
        control = GetComponent<BossControl>();
        anim = GetComponent<BossAnimation>();
        AwakeReset();
    }

    protected virtual void AwakeReset()
    {

    }

    public virtual void Action(bool _condition)
    {

    }

    public virtual void Finish()
    {
        
    }
}
