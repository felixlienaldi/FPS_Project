using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public GameObject PauseMenu;

    public float TimerInSeconds;
    public bool Gameover;
    // Use this for initialization
    void Start()
    {
        Gameover = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerInSeconds -= Time.deltaTime;

        if (TimerInSeconds <= 0f)
        {
            Gameover = true;
        }

        Pause();
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

    }

    public void ResumeButton()
    {
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitButton()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }
}
