using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour {

	public float damage = 100.0f;

	public float lifetime = 1.0f;


	// Use this for initialization
	void Start () {
		Invoke ("Die", lifetime);

	}
	
	void OnTriggerEnter2D(Collider2D otherCollider){

		if (!otherCollider.CompareTag ("Player"))
			return;

		PlayerController.Health -= damage;

	}

	public void Die(){
		Destroy (gameObject);
	}
}
