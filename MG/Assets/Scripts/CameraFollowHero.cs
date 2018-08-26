using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour {
	GameObject hero;
	Vector3 offset;
	// Use this for initialization
	void Start () {
		hero = GameObject.FindWithTag("Hero");
		offset = transform.position - hero.transform.position;
	}

	// Update is called once per frame
	void Update () {
		if (Global.isCameraFollowHero == true) {
			Vector3 delta = offset +hero.transform.position - transform.position;
			transform.Translate(delta, Space.World);
		}

	
	}
}
