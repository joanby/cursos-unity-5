using UnityEngine;
using System.Collections;

public class KillTrigger : MonoBehaviour {

	void OnTriggerEnter2D(Collider2D theObject) {
		if (theObject.tag == "Player") {
			PlayerController.sharedInstance.KillPlayer ();
		}
	}
}
