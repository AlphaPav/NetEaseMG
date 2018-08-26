using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class rotation1 : MonoBehaviour {

	private Transform tr1;
	// Use this for initialization
	void Start () {
		tr1 = gameObject.GetComponent<Transform> ();
	}
//	Vector3 v1;
//	v1=new Vector3(0,0,2f);
	// Update is called once per frame
	void Update () {
		if (Input.GetKey(KeyCode.Q)) 
		{
			tr1.Rotate (Vector3.forward, 0.1f);
		}
		if (Input.GetKey (KeyCode.E)) 
		{
			tr1.Rotate (Vector3.forward, -0.1f);
		}
	}
}
