using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuBehaviour : MainMenuBehaviour {

	public static bool isPaused;

	public GameObject pauseMenu;

	public GameObject optionsMenu;


	// Use this for initialization
	void Start () {
		ContinueGame ();
		UpdateQualityLabel ();
		UpdateVolumeLabel ();
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp ("escape")) {
			if (!optionsMenu.activeInHierarchy) {
				isPaused = !isPaused;
				Time.timeScale = (isPaused ? 0 : 1);
		
				pauseMenu.SetActive (isPaused);
			} else {
				OpenPauseMenu ();
			}
		}
	}

	public void ContinueGame(){
		isPaused = false;
		pauseMenu.SetActive (false);
		Time.timeScale = 1;
	}

	public void RestartGame(){
		SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
	}


	public void IncreaseQuality(){
		QualitySettings.IncreaseLevel ();
		UpdateQualityLabel ();
	}

	public void DecreaseQuality(){
		QualitySettings.DecreaseLevel ();
		UpdateQualityLabel ();
	}

	public void SetVolume(float value){
		AudioListener.volume = value;
		UpdateVolumeLabel ();
	}

	private void UpdateQualityLabel(){
		int currentQuality = QualitySettings.GetQualityLevel ();
		string qualityName = QualitySettings.names [currentQuality];

		optionsMenu.transform.FindChild ("QualityLevel").GetComponent<UnityEngine.UI.Text> ().text = "calidad actual: " + qualityName;

	}

	private void UpdateVolumeLabel(){

		float audioVolume = AudioListener.volume * 100;

		optionsMenu.transform.FindChild("MasterVolume").GetComponent<UnityEngine.UI.Text>().text = "volumen: "+audioVolume.ToString("f2")+"%";
	}

	public void OpenOptions(){
		pauseMenu.SetActive(false);
		optionsMenu.SetActive (true);
	}

	public void OpenPauseMenu(){
		pauseMenu.SetActive (true);
		optionsMenu.SetActive (false);
	}


}
