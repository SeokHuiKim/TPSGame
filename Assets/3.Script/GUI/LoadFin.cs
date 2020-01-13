using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LoadFin : MonoBehaviour
{
    private void Start()
    {
        Time.timeScale = 0f;
    }

    private void Update()
    {
        if (Input.anyKey)
        {
            Time.timeScale = 1f;
            Destroy(gameObject);
        }
    }
}
