using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class dpt : MonoBehaviour {

	private AudioSource mu1;
	// Use this for initialization
	void Start () {
		mu1 = gameObject.GetComponent<AudioSource>();

	}

	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hero")
		{
			mu1.Play();
		}
	}
}
