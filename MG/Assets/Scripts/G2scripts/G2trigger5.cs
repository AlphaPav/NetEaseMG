using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2trigger5 : MonoBehaviour {
	Rigidbody rb;
	float force = 400;
	bool isInArea =false;
	Vector3 totalEndPos;
	Vector3  beginPos;
	Vector3 disEndPos;
	bool isArrivetotalState =false;
	bool isArrivedisState =false;
	bool isup=true;
	int timecount=0;
	GameObject hero;
	float speed = 1.0f;
	// Use this for initialization
	void Start () {
		hero = GameObject.FindWithTag("Hero");
		rb= hero.GetComponent<Rigidbody>();
	}


    float v = 0.0f;
    public GameObject DisTar;
    public GameObject TotalTar;

    float beginY = -27f;

	// Update is called once per frame
	void Update () {
		if (isInArea) {
			if (Global.HeroState == Global.Total) {
                Physics.gravity = new Vector3(0,(4 + beginY - hero.transform.position[1]),0) * 3f;
			}
			else if (Global.HeroState == Global.Distributed) {
                Physics.gravity = new Vector3(0, (8 + beginY - hero.transform.position[1]), 0) * 3f;
            }
		}

	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Hero")
		{
			isInArea = true;
            Physics.gravity = new Vector3(0, 6f, 0);
        }
	}
	void OnTriggerExit(Collider collider)
	{
		if (collider.tag == "Hero")
		{
			isInArea = false;
            Physics.gravity = new Vector3(0, -9.81f, 0);
        }
	}

}
