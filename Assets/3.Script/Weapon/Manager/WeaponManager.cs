using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponManager : MonoBehaviour
{
    public GameObject[] firstWeapon;
    public GameObject[] secondWeapon;

    private bool isWeaponSlotChange;
    private bool isChange;
    private int weaponNumber;


    private void Start()
    {
        InitializeWeapon();
    }

    void Update()
    {
        if (!isChange)
        {
            if (Input.GetKeyDown(KeyCode.G))
            {
                isWeaponSlotChange = !isWeaponSlotChange;
                WeaponTrans();
            }
            WeaponKeySetting();
        }
    }

    private void InitializeWeapon()
    {
        ActivationOff();
        firstWeapon[0].SetActive(true);
        isWeaponSlotChange = false;
        weaponNumber = 0;
    }

    private void WeaponTrans()
    {
        weaponNumber = 0;
        WeaponChange(weaponNumber);
    }

    private void WeaponKeySetting()
    {
        for (int i = 49; i < 58; i++)
        {
            if (Input.GetKeyDown((KeyCode)i) && !isWeaponSlotChange  && firstWeapon.Length > i - 49 && weaponNumber != i - 49)
            {
                weaponNumber = i - 49;
                WeaponChange(weaponNumber);
            }
            else if (Input.GetKeyDown((KeyCode)i) && isWeaponSlotChange && secondWeapon.Length > i - 49 && weaponNumber != i - 49)
            {
                weaponNumber = i - 49;
                WeaponChange(weaponNumber);
            }
        }
    }

    private void WeaponChange(int index)
    {
        ActivationOff();
        if (!isWeaponSlotChange)
        {
            firstWeapon[index].SetActive(true);
        }
        else
        {
            secondWeapon[index].SetActive(true);
        }
        ActiveWeapon();
    }

    private void ActiveWeapon()
    {
        if (!isWeaponSlotChange)
        {
            switch (weaponNumber)
            {
                case 0:
                    WeaponActive(true, false, false, false, false);
                    break;
                case 1:
                    WeaponActive(false, true, false, false, false);
                    break;
                case 2:
                    WeaponActive(false, false, true, false, false);
                    break;
            }
        }
        else
        {
            switch (weaponNumber)
            {
                case 0:
                    WeaponActive(false, false, false, true, false);
                    break;
                case 1:
                    WeaponActive(false, false, false, false, true);
                    break;
            }
        }
    }

    private void WeaponActive(bool sword, bool rail, bool missile, bool gatling, bool beam)
    {
        SwordController.isActive = sword;
        RailGunController.isActive = rail;
        MissileController.isActive = missile;
        GatlingController.isActive = gatling;
        BeamController.isActive = beam;
    }

    private void ActivationOff()
    {
        for (int i = 0; i < firstWeapon.Length; i++)
        {
            firstWeapon[i].SetActive(false);
        }
        for (int i = 0; i < secondWeapon.Length; i++)
        {
            secondWeapon[i].SetActive(false);
        }
    }

    public int GetIndex()
    {
        return weaponNumber;
    }

    public void SetChange(bool change)
    {
        isChange = change;
    }

    public bool GetChange()
    {
        return isChange;
    }
}