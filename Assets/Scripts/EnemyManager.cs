using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyManager : MonoBehaviour {
    public float MaxEnemyKill;
    public float EnemyTimerSpawn;
    public bool  CommanderSpawner;
    public bool Victory = false;
    public GameObject Enemy;
    public GameObject Commander;
    public Transform[] SpawnLocation;
    public GameManager GameManager;
   

    // Use this for initialization
    void Start () {
        StartCoroutine(EnemySpawner(EnemyTimerSpawn));
        GameManager = GetComponent<GameManager>();
        CommanderSpawner = true;
    }
	
	// Update is called once per frame
	void Update () {

        if (GameManager.GameOver)
        {
            StopAllCoroutines();
        }

        if (VictoryCondition())
        {
            GameManager.Victory();
        }

        if (GameManager.CountEnemyKill == MaxEnemyKill && CommanderSpawner)
        {
            BossSpawner(); //Keluar Boss
        }


    }

    void BossSpawner()//Keluar Boss
    {
        if (CommanderSpawner)
        {
            Instantiate(Commander, transform.position, Quaternion.identity);
            CommanderSpawner = false;
            
        }
    }
    public bool VictoryCondition()
    {
        if (!CommanderSpawner && Victory)
        {
            return true;
        }
        else
        {
            return false;
        }
       
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
