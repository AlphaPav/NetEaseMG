using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHurt : MonoBehaviour {

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag.Equals("MedicineA"))
        {
            int hurt = Global.MedicineADamage - Global.HeroResistanceA;
            if(hurt > 0)
            {
                Global.HeroBlood -= hurt;
                Global.HeroResistanceA++;
            }
        }
    }
}
