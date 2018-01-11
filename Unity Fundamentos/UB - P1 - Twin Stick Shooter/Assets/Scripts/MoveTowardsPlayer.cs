using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveTowardsPlayer : MonoBehaviour {

	private Transform player;

	public float speed = 2.0f;

	// Use this for initialization
	void Start () {
		player = GameObject.Find ("nave").transform;
	}
	
	// Update is called once per frame
	void Update () {
		if (!PauseMenuBehaviour.isPaused) {
			Vector3 direction = player.position - this.transform.position;
			direction.Normalize ();
			this.transform.position = this.transform.position + direction * speed;
		}
	}
}
