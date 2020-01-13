using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameSetting : MonoBehaviour
{
    public Button button;
    public Sprite bloodSprite;
    public Image bloodObject;
    public GameObject crosshair, status, weaponslot, bosshealth, vCam;
    public GameObject gameOver, gameClear;
    private GameObject _player;
    void Start()
    {
        button.gameObject.SetActive(true);
        gameOver.SetActive(false);
        gameClear.SetActive(false);
        button.enabled = false;
        _player = GameObject.FindGameObjectWithTag("Player");
    }

    public IEnumerator GameOver()
    {
        bloodObject.sprite = bloodSprite;
        bloodObject.color = new Color(1, 1, 1, 0.75f);
        bloodObject.transform.SetParent(transform);
        crosshair.SetActive(false);
        status.SetActive(false);
        weaponslot.SetActive(false);
        vCam.SetActive(false);
        bosshealth.SetActive(false);

        PlayerAnimation.Instance.HitAnimation(HitState.DownGroundFront);
        GameObject playerBoom = ObjectPool.Instance.PopFromPool("MissileBoom");
        if (playerBoom != null)
        {
            playerBoom.transform.position = _player.transform.position;
            playerBoom.SetActive(true);
        }
        gameOver.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        button.enabled = true;
        Time.timeScale = 0;

        yield return new WaitForSeconds(1.5f);
    }

    public void Win()
    {
        bloodObject.gameObject.SetActive(false);
        crosshair.SetActive(false);
        status.SetActive(false);
        weaponslot.SetActive(false);
        vCam.SetActive(false);
        bosshealth.SetActive(false);

        gameClear.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        button.enabled = true;
        Time.timeScale = 0;
    }
}
