using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class trigger2out : MonoBehaviour {
	GameObject cube;
	GameObject myhero;
	Material _mat;
	float _alpha;
	bool _isFadeOut = false;
	// Use this for initialization
	void Start () {
		cube= GameObject.FindWithTag("01Move2");
		myhero= GameObject.FindWithTag("Hero");
		_mat = cube.GetComponent<Renderer>().material;
		_alpha=  _mat.color.a;

	}
	
	// Update is called once per frame
	void Update () {
		if (_isFadeOut) {
			
			if (_alpha <1) {
				_alpha += Time.deltaTime / 2;
				_mat.color = new Color(0, 0, 1, _alpha);
			} else {
				_isFadeOut = false;
			}
		}
	}
	void OnTriggerEnter(Collider collider)
	{
		if (collider.tag == "Hero")//碰撞的是quad  
		{
			_isFadeOut = true;
			_alpha = _mat.color.a;

		}
	}
}
