using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demolition : MonoBehaviour
{
    public GameObject effect;

    private bool already;

    private void Awake()
    {
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigids) rigid.isKinematic = true;
    }

    public void Hit(Rigidbody ori)
    {
        if (already) return;
        already = true;
        //총알에 Structure 태그가 닿으면 transform.parent에서 이 함수 찾아 실행
        //GetComponent<BoxCollider>().enabled = false;
        Rigidbody[] rigids = GetComponentsInChildren<Rigidbody>();
        foreach (Rigidbody rigid in rigids)
        {
            rigid.isKinematic = false;
            rigid.constraints = RigidbodyConstraints.None;
        }
        if (ori != null)
        {
            Destroy(Instantiate(effect, ori.transform.position, Quaternion.identity), effect.GetComponent<ParticleSystem>().main.startLifetime.constant);
            ori.AddExplosionForce(10f, Vector3.down, 50f);
        }
        Destroy(gameObject, 10f);
    }
}
