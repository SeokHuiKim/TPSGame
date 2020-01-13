using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour
{
    public GameObject background, loadImage;

    private bool isPause;

    private void Awake()
    {
        background.SetActive(false);
        Lect.SetCursor(true, false);
        isPause = false;
    }

    private void Start()
    {
        loadImage.SetActive(true);
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape) && !isPause)
        {
            background.SetActive(true);
            isPause = true;
            Lect.SetCursor(false,true);
            Time.timeScale = 0f;
        }
        else if(Input.GetKeyDown(KeyCode.Escape) && isPause)
        {
            ResumeButton();
        }
        else if (Input.GetKeyDown(KeyCode.F1) && isPause)
        {
            ToBriefing();
        }
    }

    public void ResumeButton()
    {
        background.SetActive(false);
        Lect.SetCursor(true, false);
        isPause = false;
        Time.timeScale = 1f;
    }

    public void ToBriefing()
    {
        //isPause = false;
        Time.timeScale = 1f;
        SceneManager.LoadScene(1);
    }
}
