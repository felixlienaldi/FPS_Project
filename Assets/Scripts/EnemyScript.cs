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
    

    public Transform Target;
    public NavMeshAgent Nav;
    public Animator Anim;
    public Rigidbody Rb;
    public PlayerMovement Player;

    public float LookRadius;
    public float Timer;
    public float AnimationTime;
    public bool DoneAttack;

    // Use this for initialization

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Nav.stoppingDistance);
    }
    void Start()
    {
        Target = PlayerManager.Instance.Player.transform;
        Nav = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
        Player = PlayerManager.Instance.Player.GetComponent<PlayerMovement>();
        HealthStored = Health;

    }
    void FixedUpdate()
    {
       
        if (Timer > 0f)
        {
            Timer -= Time.deltaTime;
        }
    }
    // Update is called once per frame
    void Update()
    {
        //Movement();
        float Distance = Vector3.Distance(Target.position, transform.position);
        Nav.SetDestination(Target.position);
        if (Distance <= LookRadius)
        {
<<<<<<< HEAD
            
=======
            Nav.isStopped = false;
>>>>>>> f3bcd76ce7df82775e2e423a0464577ff6ee9645
            Anim.SetBool("Run", true);
            Nav.SetDestination(Target.position);

            if (Distance <= Nav.stoppingDistance)
            {
<<<<<<< HEAD
                Anim.SetBool("Run", false);
            }

            if (Distance <= Nav.stoppingDistance && Timer <= 0f)
            {
                //Attack the target
                
                Anim.SetBool("Attack", true);
                GetComponentInChildren<BoxCollider>().enabled = true;
                if (AnimationTime > 0f)
                {
                    AnimationTime -= Time.deltaTime;
                }
                //Rotate direction
                if (AnimationTime <= 0f)
                {
                    DoneAttack = true;
                }
                RotateDirection();
            }

            if (DoneAttack)
            {
                Anim.SetBool("Attack", false);
                GetComponentInChildren<BoxCollider>().enabled = false;
                if (Timer <= 0f)
                {
                    Timer = 1f;
                    AnimationTime = 0.9f;
                }
                DoneAttack = false;
            }

=======
                //Attack the target
                Anim.Play("Attack_1");
                Debug.Log("Serang");
                //Rotate the direction
                RotateDirection();
            }
            else
            {
                
            }
>>>>>>> f3bcd76ce7df82775e2e423a0464577ff6ee9645
        }
        else
        {
            Timer = 0f;
            Anim.SetBool("Run", false);
            Nav.velocity = Vector3.zero;
            Nav.isStopped = true;
          
        }
      

    }

    public void RotateDirection()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Quaternion DirectionRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, DirectionRotation, Time.deltaTime * 5f);
    }

    public void Death(float health, float attack)//tempat spawn
    {
        Destroy(this.gameObject);
        Health = health;
        Attack = attack;
    }

    /*public void Movement()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Debug.Log(Direction);
        Rb.velocity = new Vector3(10f * Direction.x, 0f, 10f * Direction.z);


    }*/

    public void OnRaycastHit()
    {
        Health -= Player.Attack;
       if (Health <= 0)
       {
           Death(HealthStored, Attack);  //biar dia spawn tempat lain
       }
    }


    public void OnTriggerEnter(Collider collision)
    {
        
    }
}
    
