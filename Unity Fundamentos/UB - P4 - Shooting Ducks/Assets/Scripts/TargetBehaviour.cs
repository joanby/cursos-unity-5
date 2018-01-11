using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetBehaviour : MonoBehaviour {

	private bool beenHit;
	private Animator animator;
	private GameObject parent;

	private bool activated;

	private Vector3 originalPos;



	public float moveSpeed = 1.0f; // Velocidad de movimiento en el eje X
	public float frequency = 5.0f; // Velocidad del movimiento sinusoidal en el eje Y
	public float magnitude = 0.1f; // Amplitud del movimiento sinusoidal




	// Use this for initialization
	void Start () {
		parent = transform.parent.gameObject;
		animator = parent.GetComponent<Animator> ();
		originalPos = parent.transform.position;

		GameController._sharedInstance.targets.Add (this);
	}


	public void ShowTarget(){
		if (!activated) {
			activated = true;
			beenHit = false;
			animator.Play ("Idle");


			iTween.MoveBy (parent, iTween.Hash(
				"y", 1.4,
				"easeType", "easeInOutExpo",
				"time", 0.5,
				"oncomplete", "OnShow",
				"oncompletetarget", this.gameObject
				)
			);
		}

	}


	void OnShow(){
		StartCoroutine ("MoveTarget");
	}

	IEnumerator MoveTarget(){
		Vector3 relativeEndPosition = parent.transform.position;

		//Estoy mirando a la derecha o a la izquierda?
		if (transform.eulerAngles == Vector3.zero) {
			//Miramos hacia la derecha
			relativeEndPosition.x = 9;
		} else {
			//Miramos hacia la izquierda
			relativeEndPosition.x = -9;
		}

		float movementTime = Vector3.Distance (parent.transform.position, relativeEndPosition) * moveSpeed;

		Vector3 pos = parent.transform.position;
		float currentTime = 0.0f;

		while (currentTime < movementTime) {

			currentTime += Time.deltaTime;
			//s = +/-(1,0,0)*v*t
			pos += parent.transform.right * moveSpeed * Time.deltaTime;//cambio en el eje X
			//s = +/-(0,1,0)*sen(w*t)*A
			pos += parent.transform.up*Mathf.Sin(frequency * Time.time)*magnitude; 

			parent.transform.position = pos;

			yield return new WaitForSeconds (0);
		}

		StartCoroutine (HideTarget ());

	}


	void OnMouseDown(){
		if (!beenHit && activated) {
			GameController._sharedInstance.AddScore ();
			beenHit = true;
			animator.Play ("Flip");
			StopAllCoroutines ();
			StartCoroutine (HideTarget ());
		}
	}



	public IEnumerator HideTarget(){
		yield return new WaitForSeconds (0.5f);

		iTween.MoveBy (parent.gameObject,
			iTween.Hash (
				"y", originalPos.y - parent.transform.position.y,
				"easeType", "easeOutQuad",
				"loopType", "none",
				"time", 0.5,
				"oncomplete", "OnHidden",
				"oncompletetarget", this.gameObject 
			)
		);

	}

	void OnHidden(){

		parent.transform.position = originalPos;
		activated = false;
	}


}
