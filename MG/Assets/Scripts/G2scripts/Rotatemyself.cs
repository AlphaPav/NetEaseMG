using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotatemyself : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
		transform.Rotate (transform.forward,Time.deltaTime*200f,Space.World);
	}

}
