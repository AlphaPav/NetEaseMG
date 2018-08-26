using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FairyGUI;

public class aboutWindow : Window {

	public aboutWindow(){

	}

	protected override void OnInit(){
		this.contentPane = UIPackage.CreateObject("Package1", "aboutWindow").asCom;
	}
}
