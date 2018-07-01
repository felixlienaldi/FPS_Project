using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraBobber : MonoBehaviour {

    public float BobTimer;
    public float BobSpeed;
    public float BobDensity;
    public float BobMidPoint;
    public float Wave;
    // Use this for initialization
    void Start() {

    }

    // Update is called once per frame
    void Update() {

        Vector3 cSharpConversion = transform.localPosition;

        if (Input.GetAxis("Horizontal") == 0 && Input.GetAxis("Vertical") == 0)
        {
            BobTimer = 0f;
        }
        else
        {
            BobTimer = BobTimer + BobSpeed;
            if (BobTimer > Mathf.PI * 2)
            {
                BobTimer = BobTimer - (Mathf.PI * 2);
            }
        }

        if (BobTimer != 0)
        {
             float translateChange = Mathf.Sin(BobTimer) * BobDensity;
             cSharpConversion.y = BobMidPoint + translateChange;
        }
        else
        {
            cSharpConversion.y = BobMidPoint;
        }

        transform.localPosition = cSharpConversion;
    }
}

