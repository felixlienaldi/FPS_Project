using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public float EnemyTimerSpawn;
    public GameObject Enemy;
    public Transform[] SpawnLocation;

    // Use this for initialization
    void Start () {
        StartCoroutine(EnemySpawner(EnemyTimerSpawn));
    }
	
	// Update is called once per frame
	void Update () {
		
	}

   public IEnumerator EnemySpawner(float _EnemyTimerSpawn)
    {
        yield return new WaitForSeconds(_EnemyTimerSpawn);
        float Randomize = Random.Range(0,100);
        if (Randomize < 50)
        {
            Instantiate(Enemy, SpawnLocation[0].position, Quaternion.identity);
        }
        else
        {
            Instantiate(Enemy, SpawnLocation[1].position, Quaternion.identity);
        }
        StartCoroutine(EnemySpawner(EnemyTimerSpawn));

    }
}
