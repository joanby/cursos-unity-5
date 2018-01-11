using UnityEngine;
using System.Collections;

public class KillZone : MonoBehaviour {

	public float damage = 100.0f;


	void OnTriggerStay2D(Collider2D otherCollider){

		if (!otherCollider.CompareTag ("Player"))
			return;


		if (PlayerController.player != null) {
			PlayerController.Health -= damage * Time.deltaTime;
		}

	}


}
