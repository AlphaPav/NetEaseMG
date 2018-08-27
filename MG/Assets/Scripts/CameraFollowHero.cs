using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollowHero : MonoBehaviour {
    GameObject hero;
    Vector3 offset;
    Vector3 beginOffset;
    Vector3 G1cameraendPos;
    public main ui;
    // Use this for initialization
    Vector3 target;
    void Start () {
        hero = GameObject.FindWithTag("Hero");

        offset = transform.position - hero.transform.position;
        beginOffset = offset;
        target = transform.position;
        G1cameraendPos = transform.position;
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
            hero = GameObject.FindWithTag("Hero");
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
          
            if (G1cameraendPos != GetComponent<Camera>().transform.position) {
                GetComponent<Camera>().transform.Translate((G1cameraendPos- GetComponent<Camera>().transform.position) * 3.0f * Time.deltaTime, Space.World);
            }
            float cameradiff = Vector3.Distance (G1cameraendPos, GetComponent<Camera>().transform.position);

            if (cameradiff < 0.01f) {
                GetComponent<Camera>().transform.Translate(G1cameraendPos - GetComponent<Camera>().transform.position, Space.World);

                Global.isCameraShaking = false;
                Global.isCameraFollowHero = true;
                

            }
            if (Global.isCameraShaking == true) {
                count++;
                if(count == 80)
                {
                    Global.isCameraShaking = false;
                    Global.isCameraFollowHero = true;
                    count = 0;
                    ui.showPenti();
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


        }


        target = offset + hero.transform.position;

        if (Global.isCameraFollowHero == true) {
            if(Vector3.Distance(transform.position, target) > 0.05)
            {
                transform.Translate((target - transform.position) * 4f * Time.deltaTime);
            }
        }

    }
}
