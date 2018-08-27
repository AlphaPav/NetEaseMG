using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G2trigger1 : MonoBehaviour {

	bool TomoveCamera =false;
	bool canused =false;
	float cameraspeed = 3.0f;
	Vector3  cameraendPos;
	GameObject camera;
	GameObject myhero;
	// Use this for initialization
    void Start () {
		camera= GameObject.FindWithTag("MainCamera");
		myhero= GameObject.FindWithTag("Hero");
		cameraendPos=camera.transform.position;
		cameraendPos.x = 68f;
		cameraendPos.y = 4.06f;

	}
	
	// Update is called once per frame
	void Update () {
		if (canused) {
			if (Global.isCameraFollowHero) {
				if (Input.GetKeyDown (KeyCode.S)) {
					TomoveCamera = true;
					Global.isCameraFollowHero = false;
					Vector3 newpos = new Vector3 (68f, 4.06f, 0.0f);
					myhero.transform.position = newpos;
					canused = false;
				}
			}

		}
		if (TomoveCamera == true) {
			if (cameraendPos != camera.transform.position) {
				camera.transform.Translate((cameraendPos - camera.transform.position) * cameraspeed * Time.deltaTime, Space.World);
			}
			float cameradiff = Vector3.Distance (cameraendPos, camera.transform.position);

			if (cameradiff < 0.001f) {
				camera.transform.Translate(cameraendPos - camera.transform.position, Space.World);
				TomoveCamera = false;
				Global.isCameraFollowHero = true;

			}

		}
	}
	void OnCollisionEnter(Collision collision)
	{
		if (collision.collider.tag == "Hero") 
		{
			
			canused = true;
		}
	}
	void OnCollisionExit(Collision collision)
	{
		if (collision.collider.tag == "Hero") 
		{

			canused = false;
		}
	}
}
