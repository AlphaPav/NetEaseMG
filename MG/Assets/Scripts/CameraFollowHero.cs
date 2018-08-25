using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour {
	GameObject hero;
	Vector3 offset;
	Vector3 beginOffset;
	// Use this for initialization
	void Start () {
		hero = GameObject.FindWithTag("Hero");
		offset = transform.position - hero.transform.position;
		beginOffset = offset;

	}
	
	// Update is called once per frame
	void Update () {
		Vector3 delte = offset + hero.transform.position - transform.position;
		transform.Translate(delte, Space.World);
	
	}
}
