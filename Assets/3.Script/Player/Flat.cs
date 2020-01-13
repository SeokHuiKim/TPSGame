using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flat : Singleton<Flat>
{
    public bool isGround;

    public bool GetGround() { return isGround; }

    private void Update()
    {
        //Debug.Log(isGround);
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Ground")) isGround = true;
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Ground")) isGround = false;
    }
}
