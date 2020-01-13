using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gatling : MonoBehaviour
{
    public int damage = 1;

    private readonly float bulletSpeed = 40f;
    private readonly float bulletLifeTime = 3f;

    private readonly string _vulcanBullet = "VulcanBullet";
    private readonly string _vulcanBoom = "VulcanBoom";

    private float _elapsedTime = 0f;

    private Vector3 origin, dir;

    public void SetDirection(Vector3 _origin, Vector3 _pos)
    {
        transform.position = _origin;
        dir = _pos;
    }

    void Update()
    {
        transform.position += dir * bulletSpeed * Time.deltaTime;

        if (GetTimer() > bulletLifeTime)
        {
            ObjectPool.Instance.PushToPool(_vulcanBullet, gameObject);
            SetTimer();
        }
    }

    private float GetTimer()
    {
        return (_elapsedTime += Time.deltaTime);
    }

    private void SetTimer()
    {
        _elapsedTime = 0f;
    }

    private void OnCollisionEnter(Collision col)
    {
        ObjectPool.Instance.PushToPool(_vulcanBullet, gameObject);

        if (col.collider.CompareTag("Enemy"))
        {
            GameObject effect = ObjectPool.Instance.PopFromPool(_vulcanBoom);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
    }
}