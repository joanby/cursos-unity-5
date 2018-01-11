using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OrbBehaviour : MonoBehaviour {


	private bool collected = false;


	void OnTriggerEnter(Collider other){

		if (!collected) {
			collected = true;
			GameController.sharedInstance.CollectOrb ();
			this.GetComponent<ParticleSystem> ().Stop ();
			Invoke ("DestroyOrb", 1.5f);
		}
	}

	void DestroyOrb(){
		Destroy (this.gameObject);
	}

}
