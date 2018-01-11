using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

using UnityEngine.SceneManagement;


public class UICountdown : MonoBehaviour {

	private Text TextCountdown;

	private float CountdownTimerDuration;
	private float CountdownTimerStartTime;

	public static float TimerBonus = 0;

	public static bool IsGameFinished;

	// Use this for initialization
	void Start () {
		IsGameFinished = false;
		TextCountdown = GetComponent<Text> ();
		SetupCountdownTimer (30);
	}
	
	// Update is called once per frame
	void Update () {
		string TimerMessage = "Fin del Juego";

		if (TimerBonus > 0 && !IsGameFinished) {
			CountdownTimerStartTime += TimerBonus;
			TimerBonus = 0;
		}

		int TimeLeft = (int)CountdownTimeRemaining ();

		if (TimeLeft > 0) {
			TimerMessage = "Tiempo: " + LeadingZero (TimeLeft);
		} else {

			if (!IsGameFinished) {

				IsGameFinished = true;

				Debug.Log ("Me he quedado sin tiempo");

				Time.timeScale = 0;

				//Invoke ("RestartLevel", 2);

				RestartLevel ();

			}
		}
			

		TextCountdown.text = TimerMessage;
	}

	private void SetupCountdownTimer(float DelayInSeconds){
		CountdownTimerDuration = DelayInSeconds;
		CountdownTimerStartTime = Time.time;
	}

	private float CountdownTimeRemaining(){
		float ElapsedSeconds = Time.time - CountdownTimerStartTime;
		float TimeLeft = CountdownTimerDuration - ElapsedSeconds;
		return TimeLeft;
	}

	private string LeadingZero(int n){
		return n.ToString ().PadLeft (3, '0');
	}

	private void RestartLevel(){

		Debug.Log ("Reiniciando nivel");

		Time.timeScale = 1;

		int SceneId = SceneManager.GetActiveScene().buildIndex;
		SceneManager.LoadScene (SceneId, LoadSceneMode.Single);
	}
}
