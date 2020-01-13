using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissileBoom : MonoBehaviour
{
    private float _elapsedTime = 0f;

    private readonly string _missileBoom = "MissileBoom";

    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(_missileBoom, gameObject);
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
