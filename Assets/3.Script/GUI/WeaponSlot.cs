using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WeaponSlot : MonoBehaviour
{
    private int weaponNumber;

    private bool isChange, isSword, isRail;
    private bool isSlotChange;

    public Image[] firstSlot, secondSlot;
    public GameObject first_Slot, second_Slot;
    public Image railCoolTimeImage;

    public RailGunController rail;
    public GatlingController gatling;
    public MissileController missile;
    public BeamController beam;

    public WeaponManager manager;

    public Image current;
    public Image[] firstIcon, secondIcon;
    public float appearTime = 1.5f;
    public float speed = 1f;

    private bool start;
    private float remain;
    private Color baseCurrent;

    void Start()
    {
        Init();
    }

    void Update()
    {
        isSword = SwordController.isActive;
        WeaponKeySetting();
        SlotChange();
        ActivateSlot();
        DeActivateSlot();
    }

    private void Init()
    {
        first_Slot.SetActive(true);
        second_Slot.SetActive(false);
        railCoolTimeImage.fillAmount = 0;
        for (int i = 0; i < firstSlot.Length; i++)
        {
            firstSlot[i].color = new Color(1, 1, 1, 0);
            firstIcon[i].color = new Color(1, 1, 1, 0);
        }
        for (int i = 0; i < secondSlot.Length; i++)
        {
            secondSlot[i].color = new Color(1, 1, 1, 0);
            secondIcon[i].color = new Color(1, 1, 1, 0);
        }
        firstSlot[0].color = new Color(0, 0, 1, 0);
        baseCurrent = current.color;
        current.color = new Color(1, 1, 1, 0);
        Appear();
    }

    private void WeaponKeySetting()
    {
        for (int i = 49; i < 58; i++)
        {
            if (Input.GetKeyDown((KeyCode)i))
            {
                Appear();
                if (!isChange && firstSlot.Length > i - 49 && weaponNumber != i - 49 && !manager.GetChange())
                {
                    weaponNumber = i - 49;
                }
                else if (isChange && secondSlot.Length > i - 49 && weaponNumber != i - 49 && !manager.GetChange())
                {
                    weaponNumber = i - 49;
                }
                ActivateSlot();
            }
        }
    }

    private void SlotChange()
    {
        if (Input.GetKeyDown(KeyCode.G) && !isSlotChange)
        {
            Appear();
            isChange = !isChange;
            if (!isChange)
            {
                first_Slot.SetActive(true);
                second_Slot.SetActive(false);
            }
            else
            {
                first_Slot.SetActive(false);
                second_Slot.SetActive(true);
            }
            weaponNumber = 0;
            ActivateSlot();
        }
    }

    private void ActivateSlot()
    {
        if (!isChange)
        {
            if (isSword)
            {
                ActivateWeapon();
            }
            else if (rail.currentGun.currentBullet > 0 && RailGunController.isActive)
            {
                ActivateWeapon();
                WeaponCoolTime();
            }
            else if (missile.currentGun.currentBullet > 0)
            {
                ActivateWeapon();
            }
        }
        else
        {
            if (gatling.currentGun.currentBullet > 0)
            {
                ActivateWeapon();
            }
            else if (beam.currentGun.currentBullet > 0)
            {
                ActivateWeapon();
            }
        }
    }

    private void DeActivateSlot()
    {
        if (rail.currentGun.currentBullet == 0)
        {
            firstSlot[1].color = new Color(1, 0, 0, firstSlot[1].color.a);
        }
        if (missile.currentGun.currentBullet == 0)
        {
            firstSlot[2].color = new Color(1, 0, 0, firstSlot[2].color.a);
        }
        if (gatling.currentGun.currentBullet == 0)
        {
            secondSlot[0].color = new Color(1, 0, 0, secondSlot[0].color.a);
        }
        if (beam.currentGun.currentBullet == 0)
        {
            secondSlot[1].color = new Color(1, 0, 0, secondSlot[1].color.a);
        }
    }

    private void ActivateWeapon()
    {
        if (!isChange)
        {
            for (int i = 0; i < firstSlot.Length; i++)
            {
                firstSlot[i].color = new Color(1, 1, 1, firstSlot[i].color.a);
            }
            firstSlot[weaponNumber].color = new Color(0, 0, 1, firstSlot[weaponNumber].color.a);
        }
        else
        {
            for (int i = 0; i < secondSlot.Length; i++)
            {
                secondSlot[i].color = new Color(1, 1, 1, firstSlot[i].color.a);
            }
            secondSlot[weaponNumber].color = new Color(0, 0, 1, firstSlot[weaponNumber].color.a);
        }
    }

    private void WeaponCoolTime()
    {
        if (Input.GetMouseButtonDown(0) && !isRail && rail.currentGun.currentBullet > 0)
        {
            railCoolTimeImage.fillAmount = 1;
            StartCoroutine(CoolTime());
        }
    }

    IEnumerator RailGunCoolTime()
    {
        isRail = true;
        while(railCoolTimeImage.fillAmount > 0)
        {
            railCoolTimeImage.fillAmount -= 0.125f;
            yield return new WaitForSeconds(0.1f);
        }
        isRail = false;
    }

    private IEnumerator CoolTime()
    {
        isRail = true;
        while(railCoolTimeImage.fillAmount > 0)
        {
            railCoolTimeImage.fillAmount -= rail.currentGun.fireRate;
            if (railCoolTimeImage.fillAmount < rail.currentGun.fireRate)
            {
                railCoolTimeImage.fillAmount = 0;
            }
            yield return new WaitForFixedUpdate();
        }
        yield return new WaitForSeconds(0.8f);
        isRail = false;
    }

    public void Appear()
    {
        if (start)
        {
            remain = appearTime;
            return;
        }
        else
        {
            remain = appearTime;
            StartCoroutine(Corout());
        }
    }

    private IEnumerator Corout()
    {
        start = true;
        float alpha = 0f;
        while (current.color.a < baseCurrent.a)
        {
            alpha += speed * Time.fixedDeltaTime;
            current.color = new Color(1, 1, 1, alpha);
            for (int i = 0; i < firstSlot.Length; i++)
            {
                firstSlot[i].color = new Color(firstSlot[i].color.r, firstSlot[i].color.g, firstSlot[i].color.b, alpha);
                firstIcon[i].color = new Color(1, 1, 1, alpha);
            }
            for (int i = 0; i < secondSlot.Length; i++)
            {
                secondSlot[i].color = new Color(secondSlot[i].color.r, secondSlot[i].color.g, secondSlot[i].color.b, alpha);
                secondIcon[i].color = new Color(1, 1, 1, alpha);
            }

            yield return new WaitForFixedUpdate();
        }

        while (remain > 0f)
        {
            remain -= Time.fixedDeltaTime;

            yield return new WaitForFixedUpdate();
        }

        start = false;

        while (current.color.a > 0 && !start)
        {
            alpha -= speed * Time.fixedDeltaTime;
            current.color = new Color(1, 1, 1, alpha);
            for (int i = 0; i < firstSlot.Length; i++)
            {
                firstSlot[i].color = new Color(firstSlot[i].color.r, firstSlot[i].color.g, firstSlot[i].color.b, alpha);
                firstIcon[i].color = new Color(1, 1, 1, alpha);
            }
            for (int i = 0; i < secondSlot.Length; i++)
            {
                secondSlot[i].color = new Color(secondSlot[i].color.r, secondSlot[i].color.g, secondSlot[i].color.b, alpha);
                secondIcon[i].color = new Color(1, 1, 1, alpha);
            }

            yield return new WaitForFixedUpdate();
        }
    }

    public void SetSlotChange(bool change)
    {
        isSlotChange = change;
    }
}