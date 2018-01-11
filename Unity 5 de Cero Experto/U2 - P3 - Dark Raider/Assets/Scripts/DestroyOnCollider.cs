using UnityEngine;
using System.Collections;

public class DestroyOnCollider : MonoBehaviour {

	public string tagToCollideWith = string.Empty;

	void OnTriggerEnter2D(Collider2D otherCollider){

		if (!otherCollider.CompareTag (tagToCollideWith))
			return;

		Destroy (gameObject);
	}

}
