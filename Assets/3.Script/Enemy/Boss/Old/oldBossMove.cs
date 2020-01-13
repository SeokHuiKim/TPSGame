//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//[RequireComponent(typeof(BossControl))]
//public class BossMove : EnemyMove
//{
//    [Tooltip("부스트 사용 거리")]
//    public float boostDistance = 15f;
//    [Tooltip("부스트 이동속도 증가배율")]
//    public float boostMag = 3f;

//    private float moveSpeed;
//    private float accel;
//    private bool boosting;

//    public override void AwakeReset()
//    {
//        base.AwakeReset();
//        moveSpeed = nav.speed;
//        accel = nav.acceleration;
//    }

//    protected override IEnumerator Move()
//    {
//        while (true)
//        {
//            float dis = (transform.position - dest).sqrMagnitude;
//            if (dis < stopDistance) SetDest();
//            if (dis > Mathf.Pow(boostDistance, 2f)) Boost(true);
//            else Boost(false);

//            yield return new WaitWhile(() => control.attacking);
//        }
//    }

//    private void Boost(bool use)
//    {
//        if(use)
//        {
//            StartCoroutine(BoostSpeed(true));
//            control.anim.Play(AnimState.Boost);
//        }
//        else
//        {
//            StartCoroutine(BoostSpeed(false));
//            control.anim.Play(AnimState.Walk);
//        }
//    }

//    private IEnumerator BoostSpeed(bool up)
//    {
//        if (up)
//        {
//            while (!control.anim.GetStateInfo(AnimState.Boost)) yield return null;

//            nav.speed = moveSpeed * boostMag;
//            nav.acceleration = accel * boostMag;

//            while (boosting) yield return null;
//        }
//        else
//        {
//            nav.speed = moveSpeed;
//            nav.acceleration = accel;
//        }
//    }

//    private void SetDest()
//    {
//        dest = Lect.RandomPosOnCenter(player.position, minDis, maxDis);
//        dest = new Vector3(dest.x, 0f, dest.z);
//        nav.SetDestination(dest);
//    }
//}
