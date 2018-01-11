using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	public int health = 2;

	public Transform explosion;

	public AudioClip hitSound;

	void OnTriggerEnter2D(Collider2D theCollider){

		//Debug.Log ("He colisionado con : "+theCollider.gameObject.name);

		//El enemigo choca con el laser
		if (theCollider.gameObject.name.Contains ("Laser")) {
			LaserBehaviour laser = theCollider.gameObject.GetComponent ("LaserBehaviour") as LaserBehaviour;
			health -= laser.damage;
			Destroy (theCollider.gameObject);

			GetComponent<AudioSource> ().PlayOneShot (hitSound);
		}

		if (health <= 0) {
			Destroy (this.gameObject);
			GameController controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController> ();
			controller.KillEnemy ();
			controller.IncreaseScore (10);
			if (explosion) {

				Transform t = (Transform)Instantiate (explosion, this.transform.position, this.transform.rotation);
				
				GameObject exploder = t.gameObject;

				Destroy(exploder, 2.0f);
			}
		}


		//El enemigo choca contra la nave protagonista
		if (theCollider.gameObject.name.Contains ("nave")) {

		}



	}



}
