using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainUI : MonoBehaviour
{
    public Text text;
    public Outline outline;
    public RectTransform blackout1;
    public Image blackout2;

    public float blackoutSpeed = 3f;

    private float timeSpan, textAlpha;
    private bool click;

    private void Awake()
    {
        Lect.SetCursor(false, true);
        timeSpan = 0.5f;
    }

    private void Update()
    {
        if (Input.anyKey) Click();

        if (click) AfterClick();
        else BeforeClick();

        if(textAlpha <= 0f)
        {
            SceneManager.LoadScene(1);
        }

        text.color = new Color(0f, 0f, 0f, textAlpha);
        outline.effectColor = new Color(1f, 1f, 1f, textAlpha);
    }

    private void Click()
    {
        textAlpha = 1f;
        click = true;
    }
    private void BeforeClick()
    {
        timeSpan += Time.deltaTime;
        textAlpha = Mathf.Sin(timeSpan) * 0.25f + 0.75f;
    }
    private void AfterClick()
    {
        if (textAlpha > 0f) textAlpha -= blackoutSpeed * Time.deltaTime;
        if (blackout1.gameObject.activeSelf)
            blackout1.anchoredPosition = Vector2.LerpUnclamped(blackout1.localPosition,
                    Vector2.zero, blackoutSpeed * Time.deltaTime);
        if (blackout2.gameObject.activeSelf)
            blackout2.color = new Color(0f, 0f, 0f, 1 - textAlpha);
    }
}
