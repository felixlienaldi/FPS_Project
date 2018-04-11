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
    public float FieldOfPoint = 100f;

    public RaycastHit hit;
    public Rigidbody Rb;
    public Camera Cam;

	void Start () {
        
        
    }
	

	void Update () {
        ShootingManager();
        RotationMove();
        TranslationMove();
    }

    void RotationMove()
    {
        RotationX = Mathf.Clamp(-Input.mousePosition.y * Sensitivy, AngleMinimumX, AngleMaximumX);
        RotationY = Input.mousePosition.x * Sensitivy;
        Cam.transform.rotation = Quaternion.Euler(new Vector3(RotationX, RotationY, 0f));
       
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
        if (Input.GetKey(KeyCode.Space))
        {
            if (Physics.Raycast(Cam.transform.position, Cam.transform.forward, out hit, FieldOfPoint))
            {
                Debug.Log(hit.transform.name);
            }
        } 
      
    }
}
