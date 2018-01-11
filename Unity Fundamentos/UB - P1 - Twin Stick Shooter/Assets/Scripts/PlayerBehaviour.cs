using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerBehaviour : MonoBehaviour {

	/*Variables*/
	//MOVIMIENTO
	public float playerSpeed = 4.0f;

	private float currentSpeed = 0.0f;

	private Vector3 lastMovement = new Vector3 ();

	//DISPARO
	public Transform laser;

	public float laserDistance = .2f;

	public float timeBetweenFires = .3f;

	private float timeUntilNextFire = 0.0f;

	public List<KeyCode> shootButton;


	public AudioClip shootSound;

	private AudioSource audioSource;

	/*Métodos de Mono Behaviour*/

	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

	// Update is called once per frame
	void Update () {
		if (!PauseMenuBehaviour.isPaused) {
			Rotate ();
			Move ();
			Fire ();
		}
	}


	/*Mis funciones o métodos*/
	//Al rotar la nave, mirará el puntero de mi ratón
	void Rotate(){
	
		Vector3 worldPos = Input.mousePosition;
		worldPos = Camera.main.ScreenToWorldPoint (worldPos);

		Vector3 spaceShipPos = this.transform.position;
		float dx = spaceShipPos.x - worldPos.x;
		float dy = spaceShipPos.y - worldPos.y;

		float angle = Mathf.Atan2 (dy, dx) * Mathf.Rad2Deg;

		Quaternion rot = Quaternion.Euler (new Vector3 (0, 0, angle+90));

		this.transform.rotation = rot;
	}

	//La nave se desplazará con los controles habituales
	void Move(){

		Vector3 movement = new Vector3 (); //(0,0,0)

		movement.x += Input.GetAxis ("Horizontal");
		movement.y += Input.GetAxis ("Vertical");

		//movement tiene coordenadas x e y entre -1 y 1
		movement.Normalize ();

		//ahora movement tiene longitud 1
		if (movement.magnitude > 0) {
			//Si el usuario realmente está pulsando las teclas de movimiento
			currentSpeed = playerSpeed;
			// S = V * t, V = vector de movimiento * velocidad actual , t = tiempo delta
			this.transform.Translate (movement * Time.deltaTime * currentSpeed, Space.World);
			lastMovement = movement;
		} else {
			//Seguir con la inercia del último movimiento
			this.transform.Translate(movement * Time.deltaTime * currentSpeed, Space.World);
			currentSpeed *= 0.99f;
		}
	}


	void Fire(){
	
		foreach (KeyCode key in shootButton) {
			if (Input.GetKey (key) && timeUntilNextFire < 0) {
				timeUntilNextFire = timeBetweenFires;
				ShootLaser ();
				break;
			}
		}

		timeUntilNextFire -= Time.deltaTime;

	}


	void ShootLaser(){

		audioSource.PlayOneShot (shootSound);

		Vector3 laserPos = this.transform.position; //la posición de la nave

		float rotationAngle = this.transform.localEulerAngles.z - 90; //grados

		laserPos.x += Mathf.Cos (rotationAngle * Mathf.Deg2Rad) * laserDistance;
		laserPos.y += Mathf.Sin (rotationAngle * Mathf.Deg2Rad) * laserDistance;

		Instantiate (laser, laserPos, this.transform.rotation);

	}

}

