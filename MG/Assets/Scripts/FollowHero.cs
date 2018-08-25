using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowHero : MonoBehaviour {
	public GameObject[] follow;

	GameObject boss;
	public float xspeed = 1f;//move force
	public float jumpSpeed = 500f;//jump force
	Rigidbody rb;

	Vector3 lastPos;
	const int Pause = 10;
	int Delay = Pause;

	public Vector3[][] offset;

	void Start()
	{
		/*follow[0] = GameObject.Find("cube1");
		follow[1] = GameObject.Find("cube2");
		follow[2] = GameObject.Find("cube3");
		follow[3] = GameObject.Find("cube5");*/

		offset = new Vector3[10][];
		for (int i = 0; i < 6; i++)
		{
			offset[i] = new Vector3[Pause];
			for (int j = 0; j < Pause; j++)
			{
				offset[i][j] = new Vector3(0, 0, 0);
			}
		}

		rb = GetComponent<Rigidbody>();
		lastPos = transform.position;
	}
	void Update()
	{
		Move();

		if (Delay == 0)
		{
			for (int i = 5; i > 0; i--)
			{
				for (int j = 0; j < Pause; j++)
				{
					offset[i][j] = offset[i - 1][j];
				}
			}
			offset[0] = new Vector3[10];
			for (int j = 0; j < Pause; j++)
			{
				offset[0][j] = new Vector3(0, 0, 0);
			}
			Delay = Pause;
		}

		for (int i = 1; i < 6; i++)
		{
			follow[i].transform.Translate(offset[i][Pause-Delay]);
		}

		offset[0][Pause-Delay] = transform.position - lastPos;
		lastPos = transform.position;

		//Debug.Log(offset);



		Delay--;

	}

	void Move()
	{
		if (Input.GetKey(KeyCode.D))
		{
			transform.Translate(transform.right * xspeed * Time.deltaTime);
		}
		if (Input.GetKey(KeyCode.A))
		{
			transform.Translate(-1 * transform.right * xspeed * Time.deltaTime);
		}
		/*if (Input.GetKey(KeyCode.Space))
		{
			Vector3 force = new Vector3(0, 1, 0);
			rb.AddForce(force * jumpSpeed);
		}*/
	}
}


