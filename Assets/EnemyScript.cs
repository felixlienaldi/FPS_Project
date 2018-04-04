using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyScript : MonoBehaviour {
    public float Health = 200f;
    public float Attack = 20f;
    public Transform SpawnLocation;

    public Transform Target;
    public NavMeshAgent nav;

    public Rigidbody Rb;
	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
        //Movement();
        nav.SetDestination(Target.position);
    }

    public void Death(float health, float attack)//tempat spawn
    {
        this.transform.position = SpawnLocation.position;
        Health = health;
        Attack = attack;
    }

    public void Movement()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Debug.Log(Direction);
        Rb.velocity = new Vector3(10f * Direction.x, 0f, 10f * Direction.z);


    }
}
