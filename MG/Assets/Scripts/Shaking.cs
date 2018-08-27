using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shaking : MonoBehaviour {
	GameObject smallhero;
	public GameObject hero;

	// Use this for initialization
	void Start () {
		smallhero = GameObject.Find ("smallhero");	

	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag.Equals("Hero"))
        {
            Global.isCameraShaking = true;
			Global.isCameraFollowHero = false;
			smallhero.gameObject.SetActive(false);
			hero.active=true;
            this.gameObject.SetActive(false);
        }
    }
}
