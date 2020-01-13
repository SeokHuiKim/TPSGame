using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAnim : MonoBehaviour
{
    public virtual void Play(AnimState state) { }

    public virtual bool GetStateInfo(AnimState state) { return false; }
}
