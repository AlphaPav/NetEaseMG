using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2trigger3 : MonoBehaviour {
	int hitnum=0;
	GameObject cube;
	// Use this for initialization
	void Start () {
		cube = this.gameObject;
	}
	
	// Update is called once per frame
	void Update () {
		if (hitnum == 2) {
			cube.active = false;
		} else {
			return;
		}
		
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Hero") 
		{
			hitnum++;
		}
	}

}
