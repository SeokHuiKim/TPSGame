using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class EnemyBullet : MonoBehaviour
{
    public int damage = 10;

    public float moveSpeed;
    public float rotateSpeed;

    protected Vector3 look;
    protected bool isBreakable;
    protected Transform player;

    private PlayerHUD hud;

    private void Awake()
    {
        AwakeReset();
    }

    private void Start()
    {
        StartCoroutine(Bullet());
    }

    protected virtual void AwakeReset()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        hud = GameObject.FindGameObjectWithTag("HUD").GetComponent<PlayerHUD>();
    }

    private IEnumerator Bullet()
    {
        while (true)
        {
            Move();
            Look();

            yield return null;
        }
    }

    protected virtual void Move() { }

    protected virtual void Look()
    {
        Lect.Rotate(transform, look, rotateSpeed);
    }

    private void OnCollisionEnter(Collision col)
    {
        if (!isBreakable) return;
        if (col.transform.CompareTag("Player"))
        {
            //대미지 및 기타효과
            hud.DecreaseHp(damage);
        }
        //이펙트
        //파괴
        StopAllCoroutines();
        Destroy(gameObject);
    }
}
