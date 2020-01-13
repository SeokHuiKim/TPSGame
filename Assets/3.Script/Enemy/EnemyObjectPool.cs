using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyObjects
{
    TankShell,
    JetMissile
}

[System.Serializable]
public class EOP
{
    public string about;
    public GameObject[] objects;

    private int index;

    public void EnableObject()
    {
        objects[index].SetActive(true);
        if (index < objects.Length) index++;
        else index = 0;
    }
}

public class EnemyObjectPool : MonoBehaviour
{
    public EOP[] eop;

    public static EnemyObjectPool instance;
    private void Awake()
    {
        if(instance == null)
            instance = this;
    }

    public void EnableObject(EnemyObjects obj)
    {
        if (obj.Equals(EnemyObjects.TankShell))
            eop[0].EnableObject();
        else if (obj.Equals(EnemyObjects.JetMissile))
            eop[1].EnableObject();
    }
}
