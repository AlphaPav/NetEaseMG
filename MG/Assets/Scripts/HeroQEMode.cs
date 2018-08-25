using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroQEMode : MonoBehaviour {

    // Use this for initialization

    public GameObject MyScene;
    public float MaxAngle = 20.0f;
    public float speed = 15f;
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        float currentAngle = MyScene.transform.rotation.eulerAngles[2];
        if(currentAngle > 180)
        {
            currentAngle = currentAngle - 360;
        }

        if (Input.GetKey(KeyCode.Q))
        {
           if(currentAngle >= MaxAngle)
           {
               return;
           }
            MyScene.transform.RotateAround(transform.position, Vector3.forward, Time.deltaTime * speed);       
        }
        else if (Input.GetKey(KeyCode.E))
        {
            if (currentAngle < - MaxAngle)
            {
                return;
            }
            MyScene.transform.RotateAround(transform.position, Vector3.forward, -1 * Time.deltaTime * speed);
        }

	}
}
