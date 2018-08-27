using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infect : MonoBehaviour {
    private bool coll = false;
    private GameObject cell = null;
    // Use this for initialization
    void Start() {
        coll = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            if (coll)
            {
                cell.tag = "InfectedCell";
                Global.HeroBlood++;
                Material material = new Material(Shader.Find("Standard"));
                material.color = Color.black;
                cell.GetComponent<MeshRenderer>().material = material;
            }
        }

    }
    
    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Cell"))
        {
            coll = true;
            cell = collision.gameObject;
        }  
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Cell"))
        {
            coll = true;
            cell = collision.gameObject;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag.Equals("Cell"))
        {
            coll = false;
            cell = null;
        }
    }
}
