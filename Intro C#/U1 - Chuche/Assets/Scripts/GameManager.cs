using UnityEngine;
using System.Collections;

public enum GameState {
	menu,
	inTheGame,
	gameOver
}


public class GameManager : MonoBehaviour {

	public static GameManager sharedInstance;

	public GameState currentGameState = GameState.menu;

	public Canvas menuCanvas;
	public Canvas gameCanvas;
	public Canvas gameOverCanvas;

	public int collectedCoins = 0;

	void Awake(){
		sharedInstance = this;
	}

	void Start() {
		currentGameState = GameState.menu;
		menuCanvas.enabled = true;
		gameCanvas.enabled = false;
		gameOverCanvas.enabled = false;
	}

	void Update() {
		/*if (Input.GetButtonDown ("s")) {
			if (currentGameState != GameState.inTheGame) {
				StartGame ();
			}
		}*/

	}

	// se llama para iniciar la partida
	public void StartGame () {
		PlayerController.sharedInstance.StartGame ();
		LevelGenerator.sharedInstance.GenerateInitialBlocks ();
		ChangeGameState (GameState.inTheGame);
		ViewInGame.sharedInstance.UpdateHighScoreLabel ();
	}


	// se llama cuando el jugador muere
	public void GameOver() {
		LevelGenerator.sharedInstance.RemoveAllTheBlocks ();
		ChangeGameState (GameState.gameOver);
		ViewGameOver.sharedInstance.UpdateUI ();
	}

	// lo llamamos cuando el jugador decide finalizar y volver al menú principal
	public void BackToMainMenu() {
		ChangeGameState (GameState.menu);
	}

	void ChangeGameState(GameState newGameState) {


		if (newGameState == GameState.menu) {
			//La escena de Unity deberá msotrar el menú principal
			menuCanvas.enabled = true;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = false;

		} else if (newGameState == GameState.inTheGame) {
			//La escena de Unity debe configurarse para mostrar el juego en si
			menuCanvas.enabled = false;
			gameCanvas.enabled = true;
			gameOverCanvas.enabled = false;

		} else if (newGameState == GameState.gameOver) {
			//La escena debe mostrar la pantalla de fin de la partida.
			menuCanvas.enabled = false;
			gameCanvas.enabled = false;
			gameOverCanvas.enabled = true;
		}


		currentGameState = newGameState;

	}

	public void CollectCoin() {

		collectedCoins++;
		ViewInGame.sharedInstance.UpdateCoinsLabel ();
	}




}
