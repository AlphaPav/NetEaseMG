using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class chuansong1 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	void OnTriggerEnter(Collider other)
	{
		if (other.tag == "Hero")
		{

			Application.LoadLevel("G3");
		}
	}
}
