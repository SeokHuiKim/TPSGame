using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunBullet : MonoBehaviour
{
    public int damage = 1;

    private float bulletSpeed = 50f;
    private float bulletLifeTime = 5f;
    private float _elapsedTime = 0f;
    private string _railBullet = "RailBullet";
    private string _railEffect = "RailBoom";

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
            SetTimer();
            ObjectPool.Instance.PushToPool(_railBullet, gameObject);
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

        ObjectPool.Instance.PushToPool(_railBullet, gameObject);
        if (col.collider.CompareTag("Enemy"))
        {
            GameObject effect = ObjectPool.Instance.PopFromPool(_railEffect);
            effect.transform.position = transform.position;
            effect.SetActive(true);
        }
    }
}