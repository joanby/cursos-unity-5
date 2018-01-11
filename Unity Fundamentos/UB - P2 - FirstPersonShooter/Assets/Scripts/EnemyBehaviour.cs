using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBehaviour : MonoBehaviour {

	//Conjunto de estados posibles del enemigo
	public enum EnemyState {
		Idle,   //Quieto
		Follow, //Perseguir
		Die     //Muerto
	}

	//Estado actual del enemigo
	public EnemyState currentState;


	/*VARIABLES DEL ENEMIGO*/
	public Transform target; //OBjetivo que debe perseguir el enemigo

	public float moveSpeed = 3.0f;   // Velocidad de movimiento del enemigo
	public float rotateSpeed = 3.0f; // Velocidad de rotación del enemigo

	//idleRange >= followRange
	public float followRange = 10.0f; // Distancia de detección del enemigo
	public float idleRange   = 10.0f; // Distancia de vuelta a estado idle


	//VIDA DEL ENEMIGO
	public float health = 100.0f;
	private float currentHealth;


	// Use this for initialization
	void Start () {
		GoToNextState ();
		this.currentHealth = this.health;
	}
	
	// Update is called once per frame
	void Update () {

	}


	IEnumerator IdleState(){

		Debug.Log ("Idle: Enter");

		while (this.currentState == EnemyState.Idle) {

			if (GetDistance () < followRange) {
				this.currentState = EnemyState.Follow;
			}

			yield return 0;
		}

		Debug.Log ("Idle: Exit");
		GoToNextState ();
	}

	IEnumerator FollowState(){
	
		Debug.Log ("Follow: Enter");

		while (this.currentState == EnemyState.Follow) {

			this.transform.position = Vector3.MoveTowards (
				this.transform.position,
				this.target.position,
				this.moveSpeed * Time.deltaTime //s = v*t
			);

			RotateTowardsTarget ();

			if (GetDistance () > idleRange) {
				this.currentState = EnemyState.Idle;	
			}

			yield return 0;
		}
			
		Debug.Log ("Follow: Exit");
		GoToNextState ();
	}

	IEnumerator DieState(){

		Debug.Log ("Die: Enter");

		Destroy (this.gameObject);

		yield return 0;

	}

	//Calcula la distancia entre el enemigo y su objetivo
	float GetDistance(){
		Vector3 director = this.transform.position - this.target.position;
		return director.magnitude;
	}
	//Transiciona al siguiente estado que deba
	void GoToNextState(){
		//El nombre del método que voy a ejecutar a continuación.
		string methodName = this.currentState.ToString() + "State";

		System.Reflection.MethodInfo info = GetType ().
			GetMethod (
				methodName, 
				System.Reflection.BindingFlags.NonPublic | 
				System.Reflection.BindingFlags.Instance
			);

		StartCoroutine ((IEnumerator)info.Invoke (this, null));
	}

	void RotateTowardsTarget(){
		/*Direction indica el vector que empieza 
		 * donde el enemigo y acaba donde el objetivo*/
		Vector3 direction = this.target.position - this.transform.position;
		/*DirectionToFace indica la rotación que debo
		 * sufrir para mirar hacia el objetivo*/
		Quaternion directionToFace = Quaternion.LookRotation (direction);
		/*El ángulo de rotación viene dado por s=v*t */
		float angleToRotate = this.rotateSpeed * Time.deltaTime;

		this.transform.rotation = Quaternion.Slerp (
			this.transform.rotation,
			directionToFace,
			angleToRotate
		);
	}

	public void TakeDamage(){

		float damageToTake = 100.0f - GetDistance () * 5.0f;

		if (damageToTake < 0) {
			damageToTake = 0;
		}
		if (damageToTake > health) {
			damageToTake = health;
		}


		this.currentHealth -= damageToTake;

		if (this.currentHealth <= 0) {
			this.currentState = EnemyState.Die;
		} else {
			this.followRange  = Mathf.Max (GetDistance (), this.followRange);
			this.currentState = EnemyState.Follow;
		}

		print ("Vida actual del enemigo: " + this.currentHealth.ToString());

	}

}
