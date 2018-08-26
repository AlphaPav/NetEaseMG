using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2trigger5 : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Hero")
		{
			if (Global.HeroState == Global.Total) {
				
			}
		}
	}

}
