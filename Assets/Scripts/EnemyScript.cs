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
    public NavMeshHit NavHit;
    public Animator Anim;
    public Rigidbody Rb;
    public PlayerMovement Player;
    private EnemyManager EnemyBossDeath;

    public float LookRadius;
    public float Timer;
    public float MaxWanderTime;
    public float WanderTime;
    public float AnimationTime;
    public float MinArea;
    public float MaxArea;

    public bool DoneAttack;

    // Use this for initialization

    public void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, LookRadius);
        Gizmos.color = Color.blue;
        Gizmos.DrawWireSphere(transform.position, Nav.stoppingDistance);

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, MinArea);
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, MaxArea);
    }
    void Start()
    {
        Target = PlayerManager.Instance.Player.transform;
        Nav = GetComponent<NavMeshAgent>();
        Rb = GetComponent<Rigidbody>();
        EnemyBossDeath = GameObject.Find("GameManager").GetComponent<EnemyManager>();
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
        if (Distance <= LookRadius)
        {
            Nav.isStopped = false;
            Anim.SetBool("Run", true);
            Nav.SetDestination(Target.position);

            if (Distance <= Nav.stoppingDistance)
            {
                Anim.SetBool("Run", false);
            }

            if (Distance <= Nav.stoppingDistance && Timer <= 0f)
            {
                //Attack the target

                Anim.SetBool("Attack", true);
                if (AnimationTime > 0f)
                {
                    AnimationTime -= Time.deltaTime;
                    if(AnimationTime <= 0.3f)
                    {
                        GetComponentInChildren<BoxCollider>().enabled = true;
                    }
                }
                //Rotate direction
                if (AnimationTime <= 0f)
                {
                    
                    DoneAttack = true;
                    GetComponentInChildren<BoxCollider>().enabled = false;

                }
                
                RotateDirection();
            }
            else
            {
                Anim.SetBool("Attack", false);
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
        }
        else
        {
            Timer = 0f;
            Anim.SetBool("Run", true);
            WanderTime += Time.deltaTime;
            //Anim.SetBool("Run", false);
            //Nav.velocity = Vector3.zero;
            //Nav.isStopped = true;
            if (WanderTime >= MaxWanderTime)
            {
                Vector3 NewPos = NavMeshWanderer(transform.position, MinArea, MaxArea);
                Nav.SetDestination(NewPos);
                WanderTime = 0f;
            }

        }
    }
    
    public Vector3 NavMeshWanderer(Vector3 _OriginPos,float _MinArea, float _MaxArea)
    {
        //navmeh.sampleposition untuk mencari point terdekat
        //random.insideunitsphere untuk mencari random point dalem suatu sphere
        float Area = _MinArea + Random.Range(_MinArea,_MaxArea);
        Vector3 Direction = Random.insideUnitSphere * Area;
        Direction += _OriginPos;
        NavMesh.SamplePosition(Direction, out NavHit, _MaxArea, -1);

        return NavHit.position;
    }

    public void RotateDirection()
    {
        Vector3 Direction = (Target.position - transform.position).normalized;
        Quaternion DirectionRotation = Quaternion.LookRotation(new Vector3(Direction.x, 0, Direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, DirectionRotation, Time.deltaTime * 5f);
    }

    public void Death()//tempat spawn
    {
        if (!EnemyBossDeath.CommanderSpawner && this.gameObject.tag == "Commander")
        {
            EnemyBossDeath.Victory = true;
        }
        GameManager.CountEnemyKill++;
        Destroy(this.gameObject);
       
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
            Death();
        }
    }


    public void OnTriggerEnter(Collider collision)
    {

    }
}
