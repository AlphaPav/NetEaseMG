using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneJump : MonoBehaviour {
	float force = 250;
	int JumpNum = 0;
	Rigidbody rb;
	void Start()
	{
		rb= GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.GetKeyDown(KeyCode.Space))
		{
			JumpNum++;
			if (JumpNum > 1) return;
			if (JumpNum == 1)
			{

				rb.AddForce(Vector3.up * force);
			}
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "WalkableRood")//碰撞的是quad  
		{

			JumpNum = 0;
		}
	}
}
