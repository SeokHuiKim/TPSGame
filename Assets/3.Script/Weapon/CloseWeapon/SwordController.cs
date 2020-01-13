using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordController : CloseWeaponController
{
    public static bool isActive = true;

    public SwordCollision collision;

    private void Update()
    {
        if (isActive)
        {
            TryWield();
        }
        SwingReset();
    }

    protected override void Wield()
    {
        base.Wield();
        PlayerAnimation.Instance.SwordAttackAnimation();
    }

    public void TargetAttack()
    {
        collision.Target();
    }

    public void TargetReset()
    {
        collision.TargetReset();
    }

    private void SwingReset()
    {
        if (anim.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
        {
            isSwing = false;
        }
    }
}
