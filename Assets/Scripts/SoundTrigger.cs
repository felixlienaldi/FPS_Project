using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SoundTrigger : MonoBehaviour {
    public static bool Mute;
    public Toggle SoundToggle;
    // Use this for initialization
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
    void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
        if (SoundToggle.isOn)
        {
            Mute = true;
        }
        else
        {
            Mute = false;
        }
	}
}
