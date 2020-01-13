//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;
//using UnityEngine.AI;

//public class BossRotate : MonoBehaviour
//{
//    private EnemyControl control;
//    private NavMeshAgent nav;
//    private float angularSpeed;

//    private void Start()
//    {
//        control = GetComponent<EnemyControl>();
//        nav = GetComponent<NavMeshAgent>();
//        angularSpeed = nav.angularSpeed;
//    }

//    public void Rotate(bool isRotateToTarget)
//    {
//        if (isRotateToTarget)
//        {
//            nav.angularSpeed = 0f;
//            StartCoroutine(LookTarget());
//        }
//        else
//        {
//            StopAllCoroutines();
//            nav.angularSpeed = angularSpeed;
//        }
//    }

//    private IEnumerator LookTarget()
//    {
//        while (true)
//        {
//            Lect.Rotate(transform, control.player.position, control.rotateSpeed * 0.5f);
//            yield return null;
//        }
//    }
//}
