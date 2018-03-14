using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float Health = 100f;
    public float Attack = 20f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        if (Health <= 0)
        {
            Destroy(this.gameObject);
        }
	}
}
