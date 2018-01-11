using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStartBehaviour : MonoBehaviour {

	private GameObject player;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("Plane");
		player.GetComponent<Rigidbody2D> ().isKinematic = true;
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetKeyUp ("space") || Input.GetMouseButtonDown (0)) {
			GameController controller = GetComponent<GameController> ();
			controller.InvokeRepeating ("CreateObstacle", 1.5f, 1.0f);
			player.GetComponent<Rigidbody2D> ().isKinematic = false;

			Destroy (this);
		}


	}
}
