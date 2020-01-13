//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(BossControl))]
//public class BossAttack : EnemyAttack
//{
//    private bool animWaiting;

//    public override void Attack(bool isMelee)
//    {
//        // 레이 쏴서 닿으면 쏘고 아니면 캔슬하고 바로 이동할 수 있도록

//        rotate.Rotate(true);
//        if (control.Equals(null)) control = GetComponent<EnemyControl>();
//        control.attacking = true;

//        if (isAnimOn)
//        {
//            //애니메이션 시작
//            if (isMelee) control.anim.Play(AnimState.MeleeAttack);
//            else control.anim.Play(AnimState.RangeAttack);
//        }

//        if (animWaiting) return;
//        StartCoroutine(AnimWaiting());
//    }

//    private IEnumerator AnimWaiting()
//    {
//        animWaiting = true;

//        //애니메이션 종료 대기
//        while (!control.anim.GetStateInfo(AnimState.Walk)) yield return null;

//        rotate.Rotate(false);
//        control.attacking = false;
//        animWaiting = false;
//    }
//}
