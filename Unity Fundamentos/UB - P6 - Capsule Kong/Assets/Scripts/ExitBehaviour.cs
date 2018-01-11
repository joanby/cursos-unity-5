using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitBehaviour : MonoBehaviour {

	private ParticleSystem ps;

	// Use this for initialization
	void Start () {
		ps = GetComponent<ParticleSystem> ();
	}
	
	void OnTriggerEnter(Collider other){

		if (ps.isPlaying) {
			print ("Has ganado!");
		} else {
			print ("Recoje todos los orbes para activar el portal");
		}

	}
}
