using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModelRotate : MonoBehaviour
{
    public Transform model;

    public GameObject jet, rob;

    public float rotateSpeed = 1f;

    private void Update()
    {
        float speed = rotateSpeed * Time.deltaTime;
        model.Rotate(0, speed, 0);
    }

    public void Set(int _num)
    {
        switch (_num)
        {
            case 1:
                jet.SetActive(true);
                rob.SetActive(false);
                break;
            case 2:
                jet.SetActive(false);
                rob.SetActive(true);
                break;
        }
    }
}
