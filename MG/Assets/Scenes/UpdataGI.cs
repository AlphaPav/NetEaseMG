using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class UpdataGI : MonoBehaviour
{
    float tempTime = 0.001f;
    void Update()
    {
        if (tempTime < 1) tempTime += 0.001f;
        else
            tempTime = 0;
        this.gameObject.GetComponent<Renderer>().material.SetColor("_EmissionColor", Color.HSVToRGB(tempTime, 1, 2));
    }
}