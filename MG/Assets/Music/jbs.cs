using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jbs : MonoBehaviour {

	private AudioSource m1;
	// Use this for initialization
	void Start () {
		m1 = gameObject.GetComponent<AudioSource>();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.D)||Input.GetKeyDown(KeyCode.A)) { m1.Play(); }
		if (Input.GetKeyUp(KeyCode.D)||Input.GetKeyUp(KeyCode.A)) { m1.Stop(); }
	}
}
