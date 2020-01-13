using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Missile : MonoBehaviour
{
    public int damage = 10;

    private readonly float missileSpeed = 50f;

    private readonly float missileLifeTime = 20f;
    private float elapsedTime = 0f;

    private readonly string missileBullet = "MissileBullet";
    private readonly string missileBoom = "MissileBoom";

    private Vector3 origin, dir;

    private MissileController missile;

    private void Start()
    {
        missile = GameObject.Find("Modelling").GetComponent<MissileController>();
    }

    public void SetDirection(Vector3 _origin, Vector3 _pos)
    {
        transform.position = _origin;
        dir = _pos;
    }

    private void Update()
    {
        if (missile.hitObject != null)
        {
            Vector3 pos = missile.hitObject.transform.position;
            Vector3 missilePos = new Vector3(pos.x, pos.y + 5, pos.z);
            transform.position += (missilePos - transform.position).normalized * missileSpeed * Time.deltaTime;
            transform.rotation = Quaternion.RotateTowards(transform.rotation, Quaternion.LookRotation(missile.hitObject.transform.position - transform.position), missileSpeed);

            if (GetTimer() > missileLifeTime)
            {
                SetTimer();
                ObjectPool.Instance.PushToPool(missileBullet, gameObject);
            }
        }
        else
        {
            transform.position += dir * missileSpeed * Time.deltaTime;

            if (GetTimer() > missileLifeTime)
            {
                SetTimer();
                ObjectPool.Instance.PushToPool(missileBullet, gameObject);
            }
        }
    }

    private float GetTimer()
    {
        return (elapsedTime += Time.deltaTime);
    }

    private void SetTimer()
    {
        elapsedTime = 0f;
    }

    private void OnCollisionEnter(Collision col)
    {
        ObjectPool.Instance.PushToPool(missileBullet, gameObject);
        if (col.transform.CompareTag("Boss") || col.transform.CompareTag("BossFunnel") || col.transform.CompareTag("Ground") || col.transform.CompareTag("Buliding"))
        {
            GameObject effect = ObjectPool.Instance.PopFromPool(missileBoom);
            effect.transform.position = transform.position;
            effect.SetActive(true);

            if (col.transform.CompareTag("Boss"))
            {
                col.transform.GetComponent<BossControl>().Damage(damage, false);
            }
            else if (col.transform.CompareTag("BossFunnel"))
            {
                col.transform.GetComponent<BossFunnel>().Damage(damage);
            }
            else if (col.transform.CompareTag("Enemy"))
            {
                col.transform.GetComponent<EnemyControl>().Damage(damage);
            }
        }
    }
}
