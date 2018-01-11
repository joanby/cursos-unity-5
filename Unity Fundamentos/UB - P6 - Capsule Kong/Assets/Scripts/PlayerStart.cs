using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStart : MonoBehaviour {

	public Transform player;

	public static bool isSpawned = false;

	public static PlayerStart sharedInstance;

	// Use this for initialization
	void Start () {
		if (sharedInstance != null) {
			Destroy (sharedInstance.gameObject);
		}

		sharedInstance = this;

		if (!isSpawned) {
			SpawnPlayer ();
			isSpawned = true;
		}

	}
	
	void SpawnPlayer(){
		Transform newPlayer = Instantiate (player, this.transform.position, Quaternion.identity) as Transform;
		newPlayer.name = "Player";
	}
}
