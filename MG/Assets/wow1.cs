using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wow1 : MonoBehaviour {

	private AudioSource m1;
	// Use this for initialization
	void Start()
	{
		m1 = gameObject.GetComponent<AudioSource>();
	}
	int z=0;
	int flag=1;
	void Update()
	{
		
		if (Input.GetKeyDown (KeyCode.X)) 
		{
			m1.Play();
		}

	}



}
