using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Infect : MonoBehaviour {
    private bool coll = false;
    private GameObject cell = null;
    public main ui;
    
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
                Material[] materials = cell.GetComponent<Renderer>().materials;
                materials[0].SetFloat("_Timenow", Time.time);
                materials[0].EnableKeyword("_DISSOLVE_ON");
                materials[0].DisableKeyword("_DISSOLVE_OFF");
                materials[1].SetFloat("_Timenow", Time.time);
                materials[1].EnableKeyword("_DISSOLVE_ON");
                materials[1].DisableKeyword("_DISSOLVE_OFF");
                Destroy(cell,2);
                //cell.GetComponent<MeshRenderer>().material = material;
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
            ui.showTishi("点击'S'感染正常细胞获得生命值\r\n变成大细菌后点击'Shift'进行分裂穿过小障碍叭~");

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
