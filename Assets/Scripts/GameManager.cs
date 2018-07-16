using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [SerializeField] public static int CountEnemyKill;

    public GameObject PauseMenu;
    public GameObject GameOverMenu;
    public GameObject SecretVictoryMenu;
    public GameObject GoodVictoryMenu;
    public GameObject BadVictoryMenu;
    public AudioSource[] AllMusic;

    public float TimerInSeconds;

    public bool SoundInPause;
    public bool GameOver;

    // Use this for initializations
    void Start()
    {
        CountEnemyKill = 0;
        GameOver = false;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        TimerInSeconds -= Time.deltaTime;

        if (TimerInSeconds <= 0f)
        {
            BadVictoryMenu.SetActive(true);
            Time.timeScale = 0f;
        }

        if (GameOver)
        {
            if (!SoundTrigger.Mute)
            {
                SoundInPause = true;
            }
            GameOverMenu.SetActive(true);
            Cursor.lockState = CursorLockMode.None;
            Time.timeScale = 0f;
        }
        else
        {
            GameOverMenu.SetActive(false);
            Time.timeScale = 1f;
        }

        Pause();
    }
    void Update()
    {
        if (SoundInPause)//muteonlyinpause
        {
            for (int i = 0; i < AllMusic.Length; i++)
            {
                AllMusic[i].volume = 0f;
            }
        }
        else
        {
            for (int i = 0; i < AllMusic.Length; i++)
            {
                AllMusic[i].volume = 1f;
            }
        }

        if (SoundTrigger.Mute)//already muted
        {
            for (int i = 0; i < AllMusic.Length; i++)
            {
                AllMusic[i].volume = 0f;
            }
        }
        else
        {
            if (!SoundInPause)
            {
                for (int i = 0; i < AllMusic.Length; i++)
                {
                    AllMusic[i].volume = 1f;
                }
            }
            
        }
    }

    public void Victory()
    {

        if (!SoundTrigger.Mute)
        {
            SoundInPause = true;
        }
        GoodVictoryMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Time.timeScale = 0f;
    }

    public void Pause()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {

            if (!SoundTrigger.Mute)
            {
                SoundInPause = true;
            }
            PauseMenu.SetActive(true);
            Time.timeScale = 0f;
            Cursor.lockState = CursorLockMode.None;
        }

    }
    public void Restart()
    {
        if (!SoundTrigger.Mute)
        {
            SoundInPause = false;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("main");
    }
    public void ResumeButton()
    {
        if (!SoundTrigger.Mute)
        {
            SoundInPause = false;
        }
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void ExitButton()
    {
        if (!SoundTrigger.Mute)
        {
            SoundInPause = false;
        }
        Time.timeScale = 1f;
        SceneManager.LoadScene("Menu");
    }
}
