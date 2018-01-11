using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class ViewGameOver : MonoBehaviour {

	public static ViewGameOver sharedInstance;
	public Text coinsLabel;
	public Text scoreLabel;


	void Awake() {
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void UpdateUI(){
		if (GameManager.sharedInstance.currentGameState == GameState.gameOver) {
			coinsLabel.text = GameManager.sharedInstance.collectedCoins.ToString ();
			scoreLabel.text = PlayerController.sharedInstance.GetDistance ().ToString ("f0");
		}
	}
}
