using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuBehaviour : MonoBehaviour {

	public void LoadLevel(string levelName){
		SceneManager.LoadScene (levelName);
	}

	public void EndGame(){

		#if UNITY_EDITOR 
			UnityEditor.EditorApplication.isPlaying = false;
		#else 
			Application.Quit()
		#endif
	}

}
