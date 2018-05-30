using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TreeScript : MonoBehaviour {

    public GameObject Tree;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void GenerateTree()
    {
        for(int i = 0; i < 900; i++)
        {
            //46 27, 6 69
            GameObject tree = Instantiate(Tree, new Vector3(Random.Range(-100f, 5f), 30.8f, Random.Range(-120f, 120f)), Quaternion.identity);
            tree.transform.parent = gameObject.transform;
        }
        
    }
}
