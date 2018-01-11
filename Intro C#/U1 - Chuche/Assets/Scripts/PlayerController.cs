using UnityEngine;
using System.Collections;

public class PlayerController : MonoBehaviour {


	public static PlayerController sharedInstance;

	public float jumpForce = 6.0f;
	public float runningSpeed = 3.0f;
	private Rigidbody2D rigidBody;
	public LayerMask groundLayerMask;
	public Animator animator;

	private Vector3 startPosition;
	private string highScoreKey = "highScore";

	void Awake() {
		animator.SetBool ("isAlive", true);

		sharedInstance = this;
		rigidBody = GetComponent<Rigidbody2D> ();
		startPosition = this.transform.position;
	}


	// Use this for initialization
	public void StartGame () {
		animator.SetBool ("isAlive", true);
		this.transform.position = startPosition;
		rigidBody.velocity = new Vector2 (0, 0);
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			if (Input.GetMouseButtonDown (0)) {
				Jump ();
			}

			animator.SetBool ("isGrounded", IsOnTheFloor ());
		}
	}


	void FixedUpdate() {

		if (GameManager.sharedInstance.currentGameState == GameState.inTheGame) {
			if (rigidBody.velocity.x < runningSpeed) {
				rigidBody.velocity = new Vector2 (runningSpeed, rigidBody.velocity.y);
			}
		}
	}



	void Jump() {
		if (IsOnTheFloor ()) {
			rigidBody.AddForce (Vector2.up * jumpForce, ForceMode2D.Impulse);
		} 
	}


	bool IsOnTheFloor() {

		if (Physics2D.Raycast (this.transform.position, Vector2.down, 1.0f, groundLayerMask.value)) {
			return true;
		} else {
			return false;
		}

	}


	public void KillPlayer() {
		GameManager.sharedInstance.GameOver ();
		animator.SetBool ("isAlive", false);

		if (PlayerPrefs.GetFloat (highScoreKey, 0) < this.GetDistance ()) {

			PlayerPrefs.SetFloat (highScoreKey, this.GetDistance ());

		}

	}



	public float GetDistance() {

		float distanceTravelled = Vector2.Distance (new Vector2 (startPosition.x, 0),
			                          new Vector2 (this.transform.position.x, 0));

		return distanceTravelled;

	}


}
