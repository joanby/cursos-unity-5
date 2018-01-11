using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {


	private Rigidbody rigidbody;

	public Vector2 jumpForce = new Vector2 (0, 400);

	public float maxSpeed = 3.0f;

	public float speed = 50.0f;

	private float xMove;

	private bool canJump;

	private bool onTheGround;

	private float yPrevious;

	private bool collidingWall;

	// Use this for initialization
	void Start () {
		rigidbody = GetComponent<Rigidbody> ();
		canJump = false;
		xMove = 0.0f;
		onTheGround = false;
		yPrevious = Mathf.Floor (transform.position.y);
		collidingWall = false;
	
	}
	
	// Update is called once per frame
	void Update () {
		CheckOnTheGround ();
		Jumping ();
	}


	void FixedUpdate(){
		//Mover el jugador a izquierda y derecha
		Movement ();

		//Moveremos la cámara para que el jugador siempre quede en el centro
		Camera.main.transform.position = new Vector3 (
			this.transform.position.x,  //x del jugador
			this.transform.position.y,  //y del jugador
			Camera.main.transform.position.z //z de la cámara
		);
	}


	void Movement(){
		//Cuanto debe moverse el jugador, -1 = izquierda, 1 = derecha, 0 = parado
		xMove = Input.GetAxis ("Horizontal");

		if (collidingWall && !onTheGround) {
			xMove = 0;
		}


		if (xMove != 0) {
			//Velocidad actual del cuerpo
			float xSpeed = Mathf.Abs (xMove * rigidbody.velocity.x);
			//Si la actual aún no llega al límite, le aplicamos una fuerza de aceleración
			if (xSpeed < maxSpeed) {
				Vector3 movementForce = new Vector3 (1, 0, 0);
				movementForce *= xMove * speed;

				RaycastHit hit;
				if (!rigidbody.SweepTest (movementForce, out hit, 0.05f)) {
					rigidbody.AddForce (movementForce);
				}
			}

			//Comprobamos si nos hemos pasado de la velocidad máxima
			if (Mathf.Abs (rigidbody.velocity.x) > maxSpeed) {
				Vector3 newVelocity;
				newVelocity.x = Mathf.Sign (rigidbody.velocity.x) * maxSpeed;
				newVelocity.y = rigidbody.velocity.y;
				newVelocity.z = 0;
				rigidbody.velocity = newVelocity;
			}

		} else {

			Vector3 newVelocity = rigidbody.velocity;
			newVelocity.x *= 0.9f; //Reduce en cada frame el 10% de la velocidad
			rigidbody.velocity = newVelocity;

		}

	}

	void Jumping(){
		if (Input.GetButtonDown ("Jump")) {
			canJump = true;
		}

		if (canJump && onTheGround) {
			rigidbody.AddForce (jumpForce);
			canJump = false;
		}


	}

	void CheckOnTheGround(){
		//Miraremos si el jugador golpea algo medido desde el centro del objeto hacia abajo
		float distance = (GetComponent<CapsuleCollider>()).height/2*this.transform.localScale.y+0.01f;
		//Dirección en la que se encuentra el suelo
		Vector3 floorDirection = transform.TransformDirection(-Vector3.up);
		//Posición actual del jugador
		Vector3 origin = transform.position;
		//Si no estamos en el suelo
		if (!onTheGround) {
			//Comprobaremos si hay algo directamente bajo nuestros pies
			if (Physics.Raycast (origin, floorDirection, distance)) {
				onTheGround = true;
			}
		} else if (Mathf.Floor (transform.position.y) != yPrevious) {

			onTheGround = false;

		}

		//nuestra posición actual debe recalcularse
		yPrevious = Mathf.Floor(this.transform.position.y);



	}


	void OnDrawGizmos(){
		Debug.DrawLine (transform.position, transform.position + rigidbody.velocity, Color.red);
	}

	void OnCollisionStay(Collision collision){

		if (!onTheGround) {
			collidingWall = true;
		}

	}
	void OnCollisionExit(Collision collision){
		collidingWall = false;
	}


}
