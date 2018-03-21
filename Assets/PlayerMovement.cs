using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour {

	[HideInInspector] public float AngleMinimumX = -45f;
    [HideInInspector] public float AngleMaximumX = 45f;
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;

    public float Sensitivy;
    public float Speed;
    public float FieldOfPoint;
    public float Health = 100f;
    public float Attack = 20f;

    public RaycastHit Hit;
    public Rigidbody Rb;
    public Camera Cam;
    public GameObject Weapon;
    public EnemyScript Enemy;
    public Vector3 MousePosition;
	void Start () {
        Cursor.lockState = CursorLockMode.Locked;


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

            Rb.velocity = new Vector3(X , 0f, Z) * Speed * Time.deltaTime;
            
        }
        if (Input.GetKey(KeyCode.S))
        {

            Rb.velocity = new Vector3(X, 0f, Z) * -Speed * Time.deltaTime;

        }

    }

    void ShootingManager()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out Hit, FieldOfPoint))
            {
                Debug.Log(Hit.transform.name);
                if(Hit.collider.tag == "Enemy")
                {
                    Enemy.Health -= Attack;
                    if(Enemy.Health <= 0)
                    {
                        Enemy.Death(Enemy.Health,Enemy.Attack);  //biar dia spawn tempat lain
                    }
                }
            }
        } 
      
    }

   
}
