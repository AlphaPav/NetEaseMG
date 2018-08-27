using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class zoulu : MonoBehaviour {

	private AudioSource mu1;
	// Use this for initialization
	void Start () {
		mu1 = gameObject.GetComponent<AudioSource>();

	}

	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown(KeyCode.A)|| Input.GetKeyDown(KeyCode.D)) mu1.Play();
	}
}
