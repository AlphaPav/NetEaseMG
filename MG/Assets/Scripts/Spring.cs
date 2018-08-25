using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour {
	Rigidbody rb;
	GameObject hero;
	float force = 400;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Hero")//碰撞的是quad  
		{
			hero = GameObject.FindWithTag ("Hero");
			rb = hero.GetComponent<Rigidbody>();
			rb.AddForce(Vector3.up * force);
		}
	}

}
