using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wow : MonoBehaviour {

	private AudioSource m1;
	// Use this for initialization
	void Start()
	{
		m1 = gameObject.GetComponent<AudioSource>();
	}
	int z;
	int flag=1;
	void Update()
	{

		if (Input.GetKeyDown (KeyCode.X)) 
		{
			m1.Play();
		}

	}

}
