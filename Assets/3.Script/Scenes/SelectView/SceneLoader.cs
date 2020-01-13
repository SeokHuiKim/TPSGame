using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SceneLoader : MonoBehaviour
{
    public ModelRotate model;

    public Text stageName, stageAbout;
    public Button sallyButton;

    public GameObject loadImage;
    public Text loadText, helpText;
    public Slider loadSlider;

    public string[] helpTexts;

    private int index;

    private void Awake()
    {
        loadImage.SetActive(false);
        SelectSceneIndex(2);
    }

    private void Start() { }

    public void SelectSceneIndex(int _index)
    {
        switch (_index)
        {
            case -1:
                stageName.text = "공중전";
                stageAbout.text = "- 정보가 부족함.";
                sallyButton.interactable = false;
                model.Set(1);
                return;
            case 2:
                stageName.text = "기체 출현";
                stageAbout.text = "- 방치된 적대 이족보행병기를 처치하라.";
                model.Set(2);
                break;
        }

        index = _index;
        sallyButton.interactable = true;
    }

    public void LoadScene()
    {
        StartCoroutine(LoadAsynchronously(index));
    }

    private IEnumerator LoadAsynchronously(int index)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(index);

        loadImage.SetActive(true);
        helpText.text = helpTexts[Random.Range(0, helpTexts.Length)];

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / .9f);
            if(loadText != null && loadText.gameObject.activeSelf)
                loadText.text = GetN2(progress) * 100 + "";
            if(loadSlider != null && loadSlider.gameObject.activeSelf)
                loadSlider.value = GetN2(progress);

            yield return null;
        }
    }

    private float GetN2(float f)
    {
        string result = string.Empty;

        if (f == (int)f) result = f.ToString();
        else result = f.ToString("N2");

        return float.Parse(result);
    }
}