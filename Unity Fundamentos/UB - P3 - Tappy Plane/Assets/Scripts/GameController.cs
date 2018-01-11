using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	[HideInInspector]//oculta la variable pública del inspector de Unity
	public static float speedModifier;


	[Header("Información del obstáculo")]
	[Tooltip("El obstáculo que va a spawnearse")]
	public GameObject obstacleRef;


	[Tooltip("Mínima Y para el obstáculo")]
	public float obstacleMinY = -1.2f;


	[Tooltip("Máxima Y para el obstáculo")]
	public float obstacleMaxY = 1.2f;



	private static Text scoreText;
	private static Text maxscoreText;
	private static int score;

	public static int Score {
		get{
			return score;
		}

		set{
			score = value;
			scoreText.text = score.ToString ();

			int maxscore = PlayerPrefs.GetInt ("maxscore", 0);
			if (score > maxscore) {
				maxscoreText.text = "MaxScore\n"+score.ToString ();
				PlayerPrefs.SetInt ("maxscore", score);
			}

		}
	}


	// Use this for initialization
	void Start () {
		speedModifier = 1.0f;

		this.gameObject.AddComponent<GameStartBehaviour> ();

		score = 0;

		scoreText = GameObject.Find ("Score Text").GetComponent<Text> ();
		maxscoreText = GameObject.Find ("MaxScore Text").GetComponent<Text> ();

		maxscoreText.text = "MaxScore\n"+PlayerPrefs.GetInt ("maxscore", 0).ToString ();
	

	}



	void CreateObstacle(){

		Vector3 pos = new Vector3 (RepeatBackground.ScrollWidth,//x 
			              Random.Range (obstacleMinY, obstacleMaxY),//y
						  0.0f//z
						);
		Instantiate (obstacleRef, pos, Quaternion.identity);

	}

}
