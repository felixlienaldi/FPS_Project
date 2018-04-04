using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {
    
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;

    public float Sensitivy;
    public float Speed;
    public float JumpForce;
    public float FieldOfPoint;
    public float Health = 100f;
    public float Attack = 20f;
    public float FireRate;
    public float LastFired;


    public RaycastHit Hit;
    public Rigidbody Rb;
    public Camera Cam;
    public GameObject Weapon;
    public EnemyScript Enemy;
    public Vector3 MousePosition;
    public GameObject Bullet;

	void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0f, -100f, 0f);
        
    }
	

	void Update () {
        MousePosition.x = Input.GetAxis("Mouse X");
        MousePosition.y = Input.GetAxis("Mouse Y");
        ShootingManager();
        RotationMove();
        TranslationMove();
        
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 100, Color.green);
        Debug.DrawRay(Weapon.transform.position, Weapon.transform.forward * 100, Color.red);

    }

    void RotationMove()
    {
        RotationX = -MousePosition.y * Sensitivy;
        RotationY = MousePosition.x * Sensitivy;
        Vector3 Result = Cam.transform.eulerAngles + new Vector3(RotationX, RotationY, 0f);
        if ((Result.x < 315f && Result.x > 180f)) Result.x = 315f;
        else if (Result.x > 45f && Result.x < 180f) Result.x = 45f;
        Cam.transform.eulerAngles = Result;
    }

    void TranslationMove()
    {
        float X = Cam.transform.forward.x;
        float Z = Cam.transform.forward.z;
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(X, 0f, Z) * Speed * Time.deltaTime;
            //Rb.velocity = new Vector3(X , Rb.velocity.y , Z) * Speed * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(X, 0f, Z) * -Speed * Time.deltaTime;
            //Rb.velocity = new Vector3(X, Rb.velocity.y, Z) * -Speed * Time.deltaTime;

        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Rb.velocity = new Vector3(0f, JumpForce, 0f);

        }


    }

    void ShootingManager()
    {
        
        if (Input.GetButton("Fire1"))
        {
            StartCoroutine(ShootingRate(FireRate));
        } 
      
    }

    public IEnumerator ShootingRate(float FireRates)
    {
        yield return new WaitForSeconds(0.1f);
        if(Time.time - LastFired > 1 - FireRates)
        {
            LastFired = Time.time;
            Vector3 RaycastPosition = Cam.transform.position;
            float RaycastPositionX = Random.Range(RaycastPosition.x - 0.5f, RaycastPosition.x + 0.5f);
            float RaycastPositionY = Random.Range(RaycastPosition.y, RaycastPosition.y + 0.5f);
            float RaycastPositionZ = Random.Range(RaycastPosition.z - 0.5f, RaycastPosition.z + 0.5f);
            RaycastPosition.x = RaycastPositionX;
            RaycastPosition.y = RaycastPositionY;
            RaycastPosition.z = RaycastPositionZ;
            if (Physics.Raycast(RaycastPosition, Cam.transform.forward, out Hit, FieldOfPoint))
            {
                Debug.Log(Hit.transform.name);
                GameObject ShootingEffect = Instantiate(Bullet, Hit.point, Quaternion.LookRotation(Hit.normal));
                Destroy(ShootingEffect, 0.1f);
                if (Hit.collider.tag == "Enemy")
                {
                    Enemy.Health -= Attack;
                    if (Enemy.Health <= 0)
                    {
                        Enemy.Death(Enemy.Health, Enemy.Attack);  //biar dia spawn tempat lain
                    }
                }
            }
        }
        
       
    }

   
}
