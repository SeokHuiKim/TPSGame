using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthWarning : MonoBehaviour
{
    public float speed = 1f;

    public float minAlpha = 0.3f, maxAlpha = 0.8f;

    private Image image;

    private bool start, finish;

    private void Awake()
    {
        image = GetComponent<Image>();
        image.enabled = false;
        start = false;
    }

    public void Action(bool _flag)
    {
        if (_flag)
        {
            if (!start)
            {
                finish = false;
                start = true;
                StartCoroutine(Warning());
            }
        }
        else
        {
            finish = true;
        }
    }

    private IEnumerator Warning()
    {
        image.enabled = true;

        float alpha = 0f;
        Color color = image.color;
        bool flag = true;
        while (!finish)
        {
            if (alpha > maxAlpha)
            {
                flag = false;
            }
            else if (alpha < minAlpha)
            {
                flag = true;
            }

            if (flag)
            {
                alpha += speed * Time.fixedDeltaTime;
            }
            else
            {
                alpha -= speed * Time.fixedDeltaTime;
            }

            SetColor(color, alpha);

            yield return null;
        }

        while (alpha >= 0f)
        {
            alpha -= speed * Time.fixedDeltaTime;

            SetColor(color, alpha);

            yield return null;
        }

        image.enabled = false;
        start = false;
    }

    private void SetColor(Color _color, float _alpha)
    {
        image.color = new Color(_color.r, _color.g, _color.b, _alpha);
    }
}
