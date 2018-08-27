using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class smallhero : MonoBehaviour {
	public float xspeed = 3f;//move force
	public float jumpSpeed = 600f;//jump force

	Rigidbody rb;

	//上次位置
	Vector3 lastPos;
	int JumpNum = 0;
	float force = 250;
	// Use this for initialization
	void Start () {
		
		rb = GetComponent<Rigidbody>();
		lastPos = transform.position;

	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKey (KeyCode.D) || Input.GetKey (KeyCode.RightArrow)) {
			transform.Translate (new Vector3 (1, 0, 0) * xspeed * Time.deltaTime, Space.World);
		}
		if (Input.GetKey (KeyCode.A) || Input.GetKey (KeyCode.LeftArrow)) {
			transform.Translate (-1 * new Vector3 (1, 0, 0) * xspeed * Time.deltaTime, Space.World);
		}
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
		if (collision.collider.tag.Equals("WalkableRood"))//碰撞的是quad  
		{
			JumpNum = 0;
		}
	}
}
