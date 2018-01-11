using UnityEngine;
using System.Collections;

public class Coin : MonoBehaviour {

	public static int coinsCount = 0;


	// Use this for initialization
	void Start () {
		//Debug.Log ("Moneda creada satisfactoriamente!");
		Coin.coinsCount++;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter(Collider collider) {

		if (collider.CompareTag ("Player")) {
			Destroy (gameObject);
		}

	}


	void OnDestroy(){
		Coin.coinsCount--;

		if (Coin.coinsCount <= 0) {
			GameObject timer = GameObject.Find ("GameTimer");
			Destroy (timer);
		

			GameObject[] fireworks = GameObject.FindGameObjectsWithTag ("Firework");
			foreach (GameObject firework in fireworks) {
				firework.GetComponent<ParticleSystem> ().Play ();
			}
		}
	}
}
