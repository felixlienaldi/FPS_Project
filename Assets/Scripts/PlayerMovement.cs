using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerMovement : MonoBehaviour {
    
    [HideInInspector] public float RotationX;
    [HideInInspector] public float RotationY;

    private Vector3 MousePosition;
    private float LastFired;
    private float MaxAmmo = 7;
   

    public float Sensitivy;
    public float Speed;
    public float JumpForce;
    public float FieldOfPoint;
    public float Health = 100f;
    public float Attack = 20f;
    public float Ammo;
    public float AmmoStored;
    public float ReloadTime;
    public float FireRate;
    public float AmmoReloadCapacity;

    public bool Shooting = false;

    public RaycastHit Hit;
    public Rigidbody Rb;
    public Camera Cam;
    public GameObject Bullet;
    public GameObject Weapon;
    public Animator Anim;
    public Text AmmoText;
    public GameManager GameManager;
    public AudioSource ShotSoundEffect;
    public AudioSource ReloadSoundEffect;
    public AudioSource StabbedSoundEffet;
    public Image HealthUI;

    void Start () {
        Cursor.lockState = CursorLockMode.Locked;
        Physics.gravity = new Vector3(0f, -100f, 0f);
        AmmoText.enabled = false;
    }


    void FixedUpdate()
    {
        RotationMove();
        TranslationMove();
    }

    void Update () {
        Anim.SetBool("Melee", false);
        Anim.SetBool("Shooting", false);
        MousePosition.x = Input.GetAxis("Mouse X");
        MousePosition.y = Input.GetAxis("Mouse Y");
        WeaponManager();
        Debug.DrawRay(Cam.transform.position, Cam.transform.forward * 100, Color.green);
        Debug.DrawRay(Weapon.transform.position, Weapon.transform.forward * 100, Color.red);
       
    }
    

    void RotationMove()
    {
        RotationX = -MousePosition.y * Sensitivy * Time.fixedDeltaTime;
        RotationY = MousePosition.x * Sensitivy * Time.fixedDeltaTime;
        Vector3 Result = Cam.transform.eulerAngles + new Vector3(RotationX, RotationY, 0f);
        if ((Result.x < 315f && Result.x > 180f)) Result.x = 315f;
        else if (Result.x > 45f && Result.x < 180f) Result.x = 45f;
        Cam.transform.eulerAngles = Result;
    }

    void TranslationMove()
    {
        float Xforward = Cam.transform.forward.x;
        float Zforward = Cam.transform.forward.z;
        float Xside = Cam.transform.right.x;
        float Zside = Cam.transform.right.z;
        
        if (Input.GetKey(KeyCode.W))
        {
            transform.position += new Vector3(Xforward, 0f, Zforward) * Speed * Time.deltaTime;
            //Rb.velocity = new Vector3(X , Rb.velocity.y , Z) * Speed * Time.deltaTime;

        }
        
        if (Input.GetKey(KeyCode.S))
        {
            transform.position += new Vector3(Xforward, 0f, Zforward) * -Speed * Time.deltaTime;
           
            //Rb.velocity = new Vector3(X, Rb.velocity.y, Z) * -Speed * Time.deltaTime;

        }
        if (Input.GetKey(KeyCode.D))
        {
            transform.position += new Vector3(Xside, 0f, Zside) * Speed * Time.deltaTime;
          

        }
        if (Input.GetKey(KeyCode.A))
        {
            transform.position += new Vector3(Xside, 0f, Zside) * -Speed * Time.deltaTime;
           
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
           
            Rb.velocity = new Vector3(0f, JumpForce, 0f);
        }


    }

    void WeaponPickUp()
    {
        Vector3 RaycastPosition = Cam.transform.position;
        if (Physics.Raycast(RaycastPosition, Cam.transform.forward, out Hit, 50f))
        {
            if (Hit.collider.tag == "WeaponRange")
            {
                Destroy(Hit.transform.gameObject);
                Weapon.SetActive(true);
                Weapon.GetComponent<BoxCollider>().enabled = false;
                AmmoText.enabled = true;
                Shooting = true;
            }
        }
    }

    void AmmoPickUp()
    {
        Vector3 RaycastPosition = Cam.transform.position;
        if (Physics.Raycast(RaycastPosition, Cam.transform.forward, out Hit, 50f))
        {
            if (Hit.collider.tag == "Ammo")
            {
                Destroy(Hit.transform.gameObject);
                AmmoStored += AmmoReloadCapacity;
            }
        }
    }

    void WeaponManager()
    {
        AmmoText.text = Ammo + "/" + AmmoStored; 
        if (Input.GetButtonDown("Fire1"))
        {
            if (Shooting && Ammo > 0)
            {
                StartCoroutine(ShootingRate(FireRate));
                
            }
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            WeaponPickUp();
            AmmoPickUp();
            
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            if(AmmoStored > 0 && Shooting)
            {
                ReloadSoundEffect.Play();
                Shooting = false;
                StartCoroutine(Reload(ReloadTime));
            }
        }
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Melee Attack
            Anim.SetBool("Melee", true);
        }
        

    }

    public IEnumerator Reload(float _ReloadTime)
    {
        yield return new WaitForSeconds(_ReloadTime);
        float AmmoReload = MaxAmmo - Ammo;
        if (AmmoStored - AmmoReload < 0)
        {
            Ammo += AmmoStored;
            AmmoStored = 0;
        }
        else
        {
            Ammo += AmmoReload;
            AmmoStored -= AmmoReload;
        }
        Shooting = true;

    }

   
    public IEnumerator ShootingRate(float _FireRates)
    {

        if (Time.time - LastFired > 1 - _FireRates)
        {
            LastFired = Time.time;
            Vector3 RaycastPosition = Cam.transform.position;
            float RaycastPositionX = Random.Range(RaycastPosition.x - 0.25f, RaycastPosition.x + 0.25f);
            float RaycastPositionY = Random.Range(RaycastPosition.y, RaycastPosition.y + 0.25f);
            float RaycastPositionZ = Random.Range(RaycastPosition.z - 0.25f, RaycastPosition.z + 0.25f);
            RaycastPosition.x = RaycastPositionX;
            RaycastPosition.y = RaycastPositionY;
            RaycastPosition.z = RaycastPositionZ;
            if (Physics.Raycast(RaycastPosition, Cam.transform.forward, out Hit, FieldOfPoint) && Time.timeScale == 1f)
            {
                GameObject ShootingEffect = Instantiate(Bullet, Hit.point, Quaternion.LookRotation(Hit.normal));
                ShotSoundEffect.Play();
                Anim.SetBool("Shooting", true);
                Destroy(ShootingEffect, 0.1f);
                Ammo -= 1;
                if (Hit.collider.tag == "Enemy" || Hit.collider.tag == "Commander")
                {
                    Hit.transform.SendMessage("OnRaycastHit");

                }
            }
        }
        yield return new WaitForSeconds(0.01f);


    }

    public void OnTriggerEnter(Collider Target)
    {
        if (Target.gameObject.tag == "Water")
        {
            RenderSettings.fogColor = new Color(102f / 255f, 24f / 255f, 248f / 255f, 127f / 255f);
            RenderSettings.fogEndDistance = 100f;
        }
        if (Target.gameObject.tag == "Sword")
        {
            StabbedSoundEffet.Play();
            Health -= Target.GetComponentInParent<EnemyScript>().Attack;
            HealthUI.color += new Color(0f, 0f, 0f, 51f/255f);
            if (Health <= 0f)
            {
                GameManager.GameOver = true;
            }
        }
    }

    public void OnTriggerExit(Collider Target)
    {
        if (Target.gameObject.tag == "Water")
        {
            RenderSettings.fogColor = new Color(94f / 255f, 94f / 255f, 94f / 255f, 255f / 255f);
            RenderSettings.fogEndDistance = 15f;
        }
       
    }

}
