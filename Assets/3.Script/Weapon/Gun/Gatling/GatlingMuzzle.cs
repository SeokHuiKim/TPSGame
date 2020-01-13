using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingMuzzle : MonoBehaviour
{
    private float _elapsedTime = 0f;

    private string gatlingMuzzle = "GatlingMuzzle";

    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(gatlingMuzzle, gameObject);
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
