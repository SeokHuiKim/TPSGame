using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DisappearHUD : MonoBehaviour
{
    public float appearTime = 1.5f;

    public float speed = 1f;

    public Image[] current;
    public Image max;

    private bool start;
    private float remain;

    private Color baseCurrent;

    private void Start()
    {
        baseCurrent = current[0].color;

        Color a = new Color(1, 1, 1, 0);
        for(int i = 0; i < current.Length; i++)
        {
            current[i].color = a;
        }
        max.color = a;
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
        Color color = new Color(current[0].color.r, current[0].color.g, current[0].color.b, 0);
        while (current[0].color.a < baseCurrent.a)
        {
            color.a += speed * Time.fixedDeltaTime;
            for (int i = 0; i < current.Length; i++)
            {
                current[i].color = color;
            }
            max.color = color;

            yield return null;
        }

        while (remain > 0f)
        {
            remain -= Time.fixedDeltaTime;

            yield return null;
        }

        start = false;

        while (current[0].color.a > 0 && !start)
        {
            color.a -= speed * Time.fixedDeltaTime;
            for (int i = 0; i < current.Length; i++)
            {
                current[i].color = color;
            }
            max.color = color;

            yield return null;
        }
    }
}
