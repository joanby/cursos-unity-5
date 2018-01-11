using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;
using System;

public class UITime : MonoBehaviour {

	private Text TextClock;

	private Light Sun; 

	// Use this for initialization
	void Start () {
		TextClock = GetComponent<Text> ();

		Light[] AllTheLights = FindObjectsOfType (typeof(Light)) as Light[];
		foreach (Light Light in AllTheLights) {
			Sun = Light;
		}
	}
	
	// Update is called once per frame
	void Update () {
		DateTime Now = DateTime.Now;

		string Hour = LeadingZero(Now.Hour);
		string Minute = LeadingZero (Now.Minute);
		string Second = LeadingZero (Now.Second);

		TextClock.text = Hour + ":" + Minute + ":" + Second;

		Sun.transform.eulerAngles = new Vector3 ( 6*Now.Minute + Now.Second/ 10.0f ,0,0);
	}


	string LeadingZero(int n){
		return n.ToString ().PadLeft (2, '0');
	}
}
