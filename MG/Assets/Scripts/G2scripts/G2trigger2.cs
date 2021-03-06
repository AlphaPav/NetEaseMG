﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2trigger2 : MonoBehaviour {

	GameObject cube;
	bool notused =true;
	const bool ON = true;
	const bool OFF = false;
	bool TomoveCamera = OFF;
	bool BackmoveCamera = OFF;
	bool state = OFF;
	float speed = 3.0f;
	float cameraspeed = 5.0f;

	Vector3 beginPos;
	Vector3 endPos;
	Vector3 cameraendPos;
	Vector3 camerabeginPos;
	Vector3 offset = new Vector3(1.1f,0,0); 
	GameObject camera;

	// Use this for initialization
	void Start () {
		cube=GameObject.FindWithTag("02Move2");
		camera= GameObject.FindWithTag("MainCamera");
		if (camera == null)
			Debug.Log ("MainCamera null");
		beginPos = cube.transform.position;
		endPos = beginPos + offset;

		cameraendPos=camera.transform.position;
		cameraendPos.x = 45.66f;
		cameraendPos.y = 1f;
	}
	
	// Update is called once per frame
	void Update () {

		if (state == OFF) {
			if (TomoveCamera == ON) {
				if (cameraendPos != camera.transform.position) {

					camera.transform.Translate((cameraendPos - camera.transform.position) * cameraspeed * Time.deltaTime, Space.World);
				}
				float cameradiff = Vector3.Distance (cameraendPos, camera.transform.position);

				if (cameradiff < 0.001f) {
					camera.transform.Translate(cameraendPos - camera.transform.position, Space.World);

					TomoveCamera = OFF;
					state = ON;
				}

			} else if (BackmoveCamera == ON) {
				if (camerabeginPos != camera.transform.position) {
					camera.transform.Translate ((camerabeginPos - camera.transform.position) * cameraspeed * Time.deltaTime, Space.World);
				}
				float cameradiff1 = Vector3.Distance (camerabeginPos, camera.transform.position);

				if (cameradiff1 < 0.001f) {
					camera.transform.Translate (camerabeginPos - camera.transform.position, Space.World);
					BackmoveCamera = OFF;
					Global.isCameraFollowHero = true;
				}
			}
			return;

		} else {
			if (endPos != cube.transform.position)
			{
				cube.transform.Translate((endPos - cube.transform.position) * speed * Time.deltaTime, Space.World);
			}


			//允许0.001的误差
			float diff = Vector3.Distance(endPos, cube.transform.position);

			if (diff < 0.001f)
			{
				cube.transform.Translate(endPos - cube.transform.position, Space.World);

				//机关任务完成
				state = OFF;
				BackmoveCamera = ON;
			}

		}
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Hero")//碰撞的是quad  
		{
			if (notused == true) {
				TomoveCamera = ON;
				Global.isCameraFollowHero = false;
				camerabeginPos=camera.transform.position;
				notused = false;
			}
		}
	}
}
