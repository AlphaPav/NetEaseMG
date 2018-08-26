using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour {

	private GComponent mainUI;
	
	// Use this for initialization
	void Start () {
		mainUI = GetComponent<UIPanel>().ui;
		mainUI.GetChild("startbt").onClick.Add(()=>{SceneManager.LoadScene("02");});	
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
