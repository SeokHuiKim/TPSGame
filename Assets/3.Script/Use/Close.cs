using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Close : MonoBehaviour
{
    public Transform test, test2, camSet;
    public float distance = 25f;
    public float max = 20f, speed = 100f;
    public float upRotLimit = 15f, downRotLimit = -10f;

    [HideInInspector]
    public float count;

    private CameraController ctrl;
    private Transform player;
    private Vector3 pos;
    private bool once;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        ctrl = player.GetComponent<CameraController>();
    }

    private void Update()
    {
        pos = player.position + Vector3.up * 3f;

        RaycastHit hit;
        if (Physics.Raycast(pos, -player.transform.forward, out hit, distance, 1 << 13))
        {
            if (!once)
            {
                ctrl.Close(true, upRotLimit, downRotLimit);
                once = true;
            }

            if (hit.transform.tag.Equals("Buliding"))
            {
                if (count < max) count += speed * Time.deltaTime;
            }
        }
        if (!Physics.Raycast(pos, -player.transform.forward, distance + 10, 1 << 13))
        {
            if (once)
            {
                ctrl.Close(false, 0f, 0f);
                count = 0f;
                once = false;
            }
        }

        camSet.position = Vector3.Lerp(test2.position, test.position, count / max);
    }
}
