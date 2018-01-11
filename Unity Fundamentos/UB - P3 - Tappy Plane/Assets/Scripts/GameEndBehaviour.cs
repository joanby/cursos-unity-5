using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameEndBehaviour : MonoBehaviour {

	private bool canQuit = false;

	// Use this for initialization
	void Start () {
		StartCoroutine (DelayQuit ());

		GameController controller = GameObject.Find ("GameController").
			GetComponent<GameController> ();
		controller.CancelInvoke ();


		AdsController.ShowAd ();
	}
	
	// Update is called once per frame
	void Update () {
		if ((Input.GetKeyUp ("space") || Input.GetMouseButtonDown (0)) && canQuit) {
			SceneManager.LoadScene (SceneManager.GetActiveScene ().name);
		}
	}

	IEnumerator DelayQuit(){
		yield return new WaitForSeconds (1.0f);
		canQuit = true;
	}
}
