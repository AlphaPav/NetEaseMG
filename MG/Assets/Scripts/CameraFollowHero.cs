using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour {
	GameObject hero;
	Vector3 offset;
	Vector3 beginOffset;
	// Use this for initialization

	Vector3 target;
	void Start () {
		hero = GameObject.FindWithTag("Hero");
		offset = transform.position - hero.transform.position;
		beginOffset = offset;
		target = transform.position;
	}

	// Update is called once per frame
	void Update () {
		target = offset + hero.transform.position;

		if (Global.isCameraFollowHero == true) {
			if(Vector3.Distance(transform.position, target) > 0.05)
			{
				transform.Translate((target - transform.position).normalized * 3.0f * Time.deltaTime);
			}
		}

	}
}
