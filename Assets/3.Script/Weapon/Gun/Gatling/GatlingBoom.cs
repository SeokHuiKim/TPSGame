using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GatlingBoom : MonoBehaviour
{
    private float _elapsedTime = 0f;

    private string gatlingBoom = "GatlingBoom";

    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(gatlingBoom, gameObject);
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
