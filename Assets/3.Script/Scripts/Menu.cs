using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Menu : MonoBehaviour
{
    public Image gameStart;
    public Image option;
    public Image exit;

    private void Start()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
    }

    public void GameStart()
    {
        SceneManager.LoadScene("GameView");
    }

    public void Option()
    {

    }

    public void Exit()
    {
        Application.Quit();
    } 
}
