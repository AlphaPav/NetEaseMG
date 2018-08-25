using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoubleJump : MonoBehaviour {



	float force = 250;
	bool isJump = false;
	bool isDoubleJump = false;
	int JumpNum = 0;
	Rigidbody rb;
	void Start()
	{
		rb= GetComponent<Rigidbody>();
	}

	void Update()
	{
		if (Input.GetKeyUp(KeyCode.Space))
		{
			JumpNum++;
			if (JumpNum > 2) return;
			if (JumpNum == 1)
			{

				rb.AddForce(Vector3.up * force);
			}
			else if (JumpNum == 2)
			{

				rb.velocity = new Vector3(rb.velocity.x, 0, rb.velocity.z);
				rb.AddForce(Vector3.up * force);
			}
		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.name == "Quad")//碰撞的是quad  
		{

			JumpNum = 0;
		}
	}
}
