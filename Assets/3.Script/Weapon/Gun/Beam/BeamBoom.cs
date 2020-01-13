using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamBoom : MonoBehaviour
{
    private float _elapsedTime = 0f;
    private readonly string _beamBoom = "BeamBoom";

    void Update()
    {
        if (GetTimer() > gameObject.GetComponent<ParticleSystem>().main.startLifetime.constant)
        {
            ObjectPool.Instance.PushToPool(_beamBoom, gameObject);
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
        GameObject effect = ObjectPool.Instance.PopFromPool(_beamBoom);
        effect.transform.position = transform.position;
        effect.SetActive(true);
    }
}
