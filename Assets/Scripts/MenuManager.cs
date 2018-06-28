using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

    public GameObject HowToPlay;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
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
}
