using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject HowToPlay;
    public GameObject Credits;
    public AudioSource MenuMusic;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (SoundTrigger.Mute)
        {
            MenuMusic.volume = 0f;
        }
        else
        {
            MenuMusic.volume = 1f;
        }
	}

    public void PlayButton()
    {
        SceneManager.LoadScene("main");
    }

    public void HowToPlayButton()
    {
        HowToPlay.SetActive(true);
    }

    public void ExitHowToPlayButton()
    {
        HowToPlay.SetActive(false);
    }

    public void QuitButton()
    {
        Application.Quit();
    }

    public void CreditsButton()
    {
        Credits.SetActive(true);
    }
    
    public void ExitCreditsButton()
    {
        Credits.SetActive(false);
    }
}
