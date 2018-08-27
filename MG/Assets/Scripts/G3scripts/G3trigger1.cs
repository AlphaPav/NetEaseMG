using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class G3trigger1 : MonoBehaviour {
	bool isUsed = false;
	bool doing =false;
	GameObject  cubeobj;  
	GameObject water;
	Vector3 waterbeginPos;
	Vector3 waterendPos;
	Vector3 cubebeginPos;
	Vector3 cubeendPos;
	// Use this for initialization
	void Start () {
     	cubeobj = GameObject.FindWithTag ("03Move1");
		cubebeginPos = cubeobj.transform.position;
		cubeendPos = cubebeginPos;
		cubeendPos.y += 2f;

		water = GameObject.FindWithTag ("03Move2");
		waterbeginPos = water.transform.position;
		waterendPos = waterbeginPos;
		waterendPos.y -= 4f;
	}
	
	// Update is called once per frame
	void Update () {
		if (doing==true) {

			if (cubeendPos != cubeobj.transform.position) {

				cubeobj.transform.Translate(new Vector3(0,0.01f,0), Space.World);
			}
			float diff = Vector3.Distance (cubeendPos , cubeobj.transform.position);
			if (diff < 0.001f) {
				cubeobj.transform.Translate(cubeendPos  - cubeobj.transform.position, Space.World);
				doing = false;
			}

			if (doing) {
				if (waterendPos != water.transform.position) {
					water.transform.Translate(new Vector3(0,-0.1f,0), Space.World);
				}
				float waterdiff = Vector3.Distance (waterendPos,water.transform.position);
				if (waterdiff < 0.001f) {
					water.transform.Translate(waterendPos-water.transform.position, Space.World);
					water.transform.position = waterbeginPos;
				}
			}
		}


		
	}

	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Hero")//碰撞的是quad  
		{
			
			if (isUsed == false) {
				doing = true;
				isUsed = true;
			}
		}
	}



}
