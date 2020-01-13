using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAction : MonoBehaviour
{
    protected EnemyControl control;

    [HideInInspector]
    public bool wait;

    private void Awake()
    {
        AwakeReset();
    }

    public virtual void AwakeReset()
    {
        control = GetComponent<EnemyControl>();
    }

    public virtual void EnableState() { }
    public virtual void DisableState() { enabled = false; }
}
