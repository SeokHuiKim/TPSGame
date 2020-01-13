using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CloseWeaponController : MonoBehaviour
{
    public CloseWeapon currentCloseWeapon;
    public BossControl boss;
    public Animator anim;

    protected bool isSwing;

    protected void TryWield()
    {
        if (Input.GetMouseButtonDown(0) && !isSwing)
        {
            Wield();
        }
    }

    protected virtual void Wield()
    {

    }

    public void SetSwing(bool swing)
    {
        isSwing = swing;
    }
}
