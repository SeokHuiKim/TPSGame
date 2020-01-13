using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RailGunBoom : MonoBehaviour
{
    private float _elapsedTime = 0f;
    private readonly string _railBoom = "RailBoom";
    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(_railBoom, gameObject);
            SetTimer();
        }
    }

    private float GetTimer() { return (_elapsedTime += Time.deltaTime); }
    private void SetTimer() { _elapsedTime = 0f; }
}
