using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileSmoke : MonoBehaviour
{
    private float _elapsedTime = 0f;

    private string _missileLaunch = "MissileLaunchSmoke";

    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(_missileLaunch, gameObject);
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
}
