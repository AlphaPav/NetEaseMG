using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger3 : MonoBehaviour {
	bool isused =false;
	GameObject cube;
	// Use this for initialization
	void Start () {
		cube = this.gameObject;
		if (cube == null)
			Debug.Log ("cube=null");
		//Debug.Log (Global.HeroResistanceA);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Hero")
		{
			
			if (isused == false) {
				Global.HeroResistanceA++;
				//Debug.Log (Global.HeroResistanceA);
				isused = true;
				cube.active = false;
			}

		}
	}

}
