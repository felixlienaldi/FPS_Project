using System.Collections;
using System.Collections.Generic;
using UnityEngine.AI;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    private float Randomize;

    public float Health = 200f;
    public float Attack = 20f;
    public float HealthStored;

    public Transform[] SpawnLocation;

    public Transform Target;
    public NavMeshAgent Nav;

    public Rigidbody Rb;
    public PlayerMovement Player;

    // Use this for initialization
    void Start()
    {
        HealthStored = Health;

    }

    // Update is called once per frame
    void Update()
    {
        //Movement();
        Nav.SetDestination(Target.position);

    }

    public void Death(float health, float attack)//tempat spawn
    {
        Randomize = Random.Range(0, 100);
        if (Randomize < 50)
        {
            Nav.Warp(SpawnLocation[0].position);
        }
        else
        {
            Nav.Warp(SpawnLocation[1].position);
        }

        Health = health;
        Attack = attack;
    }

    public void Movement()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Debug.Log(Direction);
        Rb.velocity = new Vector3(10f * Direction.x, 0f, 10f * Direction.z);


    }

    public void OnRaycastHit()
    {
        Health -= Player.Attack;
       if (Health <= 0)
       {
           Death(HealthStored, Attack);  //biar dia spawn tempat lain
       }
    }
}
    
