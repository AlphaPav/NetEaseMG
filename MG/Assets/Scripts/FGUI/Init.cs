using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;
using UnityEngine.SceneManagement;

public class Init : MonoBehaviour {

	private GComponent mainUI;
	private aboutWindow aboutwin;
	
	// Use this for initialization
	void Start () {
		mainUI = GetComponent<UIPanel>().ui;
		aboutwin = new aboutWindow();
		aboutwin.SetXY(0, Screen.height/5);
		mainUI.GetChild("startbt").onClick.Add(()=>{SceneManager.LoadScene(1);});
		mainUI.GetChild("aboutbt").onClick.Add(()=>{aboutwin.Show();});	
		mainUI.GetChild("quitbt").onClick.Add(()=>{Application.Quit();});
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
