using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateAll : MonoBehaviour {
	GameObject pivot;
	// Use this for initialization
	void Start () {
		pivot = GameObject.FindWithTag ("02Rotate");
	}
	
	// Update is called once per frame
	void Update () {
		//this.transform.Rotate(pivot.transform.forward, 25* Time.deltaTime, Space.World);
 transform.RotateAround(pivot.transform.position, new Vector3(0, 0, 1), 30f * Time.deltaTime);

}
}
