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

    public List<KeyCode> Key = new List<KeyCode>();

    public Rigidbody Rb;
    public Camera cam;

	void Start () {
        Cursor.visible = false;

	}
	

	void Update () {
        RotationMove();
        TranslationMove();
    }

    void RotationMove()
    {
        RotationX = Mathf.Clamp(-Input.mousePosition.y * Sensitivy, AngleMinimumX, AngleMaximumX);
        RotationY = Input.mousePosition.x * Sensitivy;
        cam.transform.rotation = Quaternion.Euler(new Vector3(RotationX, RotationY, 0f));
       
    }

    void TranslationMove()
    {
         
        if (Input.GetKey(KeyCode.W))
        {
            Rb.velocity = new Vector3(0f, 0f, Speed);
            
        }   
    }
}
