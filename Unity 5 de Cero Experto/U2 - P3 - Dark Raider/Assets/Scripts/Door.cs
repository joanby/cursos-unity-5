using UnityEngine;
using System.Collections;

public class Door : MonoBehaviour {

	public int levelID = -1;

	void OnTriggerEnter2D(Collider2D otherCollider){
		if (!otherCollider.CompareTag ("Player"))
			return;

		Application.LoadLevel (levelID);
	}

}
