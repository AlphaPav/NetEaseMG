﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class main : MonoBehaviour {

	private GComponent mainUI;
    private GProgressBar Abar;
    private GProgressBar Bbar;
    private GProgressBar Cbar;
    private GTextField Text;

    private float currentTime;
    private float endTime = 6.0f;
    public bool penti = false;

	// Use this for initialization
	void Start () {
		mainUI = GetComponent<UIPanel>().ui;
        // text
        Text = mainUI.GetChild("n2").asTextField;
        Text.text="x 15";
        // progressbar 1
        Abar = mainUI.GetChild("n4").asProgress;
        // prograssbar 2
        Bbar = mainUI.GetChild("n5").asProgress;
        // prograssbar 3
        Cbar = mainUI.GetChild("n6").asProgress;
        Abar.max = 10;
        Bbar.max = 10;
        Cbar.max = 10;
        Abar.value = 0;
        Bbar.value = 0;
        Cbar.value = 0;
        mainUI.GetChild("n7").visible = false;
        currentTime = 100.0f;
	}
	
	// Update is called once per frame
	void Update () {
        Text.text = "x " + Global.HeroBlood;
        Abar.TweenValue(Global.HeroResistanceA, 1.0f);
        Bbar.TweenValue(Global.HeroResistanceB, 1.0f);
        Cbar.TweenValue(Global.HeroResistanceC, 1.0f);
        currentTime += 0.1f;
        if(penti && currentTime > endTime)
        {
            mainUI.GetChild("n7").visible = false;
            penti = false;
        }
	}

    public void showPenti()
    {
        mainUI.GetChild("n7").visible = true;
        currentTime = 0.0f;
        penti = true;
    }
}