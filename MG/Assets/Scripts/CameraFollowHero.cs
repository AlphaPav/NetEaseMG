using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour {
	GameObject hero;
	Vector3 offset;
	Vector3 beginOffset;
	// Use this for initialization
	Vector3 target;
	void Start () {
		hero = GameObject.FindWithTag("Hero");
		offset = transform.position - hero.transform.position;
		beginOffset = offset;
		target = transform.position;

        Global.isCameraShaking = false;
    }


    int count = 0;
    Vector3 randomOffect;
    Vector3 randomRotate;

    float offsetScale = 0.1f;    // 0.1 -- 0.3
    float RotateScale = 2f;     // 2 --- 5

    // Update is called once per frame
    void Update () {
       
        if (Global.isCameraShaking)
        {
            if(count % 2 == 0)
            {
                randomOffect = new Vector3(Random.Range(-offsetScale, offsetScale), Random.Range(-offsetScale, offsetScale), Random.Range(-offsetScale, offsetScale));
                randomRotate = new Vector3(Random.Range(-RotateScale, RotateScale), Random.Range(-RotateScale, RotateScale), Random.Range(-RotateScale, RotateScale));

                transform.Translate(randomOffect);
                transform.Rotate(randomRotate);
            }
            else
            {
                transform.Translate(-randomOffect);
                transform.Rotate( - randomRotate);
            }
            count++;
            if(count == 80)
            {
                Global.isCameraShaking = false;
                count = 0;
            }
            else
            {
                if(count > 40)
                {
                    offsetScale -= 0.2F / 40;
                    RotateScale -= 3F / 40;
                }
                else
                {
                    offsetScale += 0.2F / 40;
                    RotateScale += 3F / 40;
                }
                
            }
        }


		target = offset + hero.transform.position;
        Debug.Log(hero.GetComponent<Rigidbody>().velocity.magnitude);
		if (Global.isCameraFollowHero == true) {
			if(Vector3.Distance(transform.position, target) > 0.05)
			{
				transform.Translate((target - transform.position) * 4f * Time.deltaTime);
			}
		}

	}
}
