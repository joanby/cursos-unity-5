using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerBehaviour : MonoBehaviour {

	[Tooltip("La fuerza con la que se impulsa el avión")]
	public Vector2 jumpForce = new Vector2(0,300);

	//Variable para saber si hemos chocado, 
	//en cuyo caso no podremos saltar más.
	private bool beenHit;

	//El cuerpo rígido en 2D sobre el cual aplicaremos las físicas.
	private Rigidbody2D rigidbody2D;

	// Use this for initialization
	void Start () {
		beenHit = false;
		rigidbody2D = GetComponent<Rigidbody2D> ();
	}
	

	//Se ejecuta después del Update
	void LateUpdate(){

		//Saltamos con espacio o ratón si no hemos muerto antes
		if ((Input.GetKeyDown ("space") || Input.GetMouseButtonDown (0)) && !beenHit) {
			//Freno el avion antes de hacerle saltar
			rigidbody2D.velocity = Vector2.zero;
			//Le aplico la fuerza de salto al avión
			rigidbody2D.AddForce(jumpForce);
		}
			
	}


	void OnCollisionEnter2D(Collision2D other){
		//Indico que he sido tocado por un objeto
		GameController.speedModifier = 0;
		beenHit = true;
		//Paro la animación
		GetComponent<Animator> ().speed = 0.0f;

		if (!gameObject.GetComponent<GameEndBehaviour> ()) {
			gameObject.AddComponent<GameEndBehaviour> ();
		}

	}

}
