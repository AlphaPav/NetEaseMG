using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2 : MonoBehaviour {
	GameObject cube;
	GameObject myhero;
	Material _mat;

	float _alpha = 1;
	bool _isFadeIn = false;

	// Use this for initialization
	void Start () {
		cube= GameObject.FindWithTag("01Move2");
		myhero= GameObject.FindWithTag("Hero");
		_mat = cube.GetComponent<Renderer>().material;
		_mat.color = new Color(1, 0, 0, _alpha);
	}
	
	// Update is called once per frame
	void Update () {
		
		if (_isFadeIn) {
			if (_alpha > 0) {
				_alpha -= Time.deltaTime / 2;
				_mat.color = new Color(0, 0, 1, _alpha);
			} else {
				_isFadeIn = false;
			}
		} 


	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Hero")//碰撞的是quad  
		{
				_isFadeIn = true;
		    	_alpha=  _mat.color.a;
		}
	}

}
