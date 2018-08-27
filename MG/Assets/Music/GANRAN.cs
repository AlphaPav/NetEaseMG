using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GANRAN : MonoBehaviour {

	private AudioSource m1;
	// Use this for initialization
	void Start () {
		m1 = gameObject.GetComponent<AudioSource>();
	}
	
	void OnCollisionStay(Collider other)
	{
		if (other.tag == "Hero")
		{
			
			m1.Play();
		}
	}



}
