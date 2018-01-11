using UnityEngine;
using System.Collections;

public class AmmoSpawner : MonoBehaviour {

	public GameObject ammoPrefab = null;

	private Transform theTransform = null;

	public Vector2 timeDelayRange = Vector2.zero;

	public float ammoLifetime = 2.0f;

	public float ammoSpeed = 4.0f;

	public float ammoDamage = 100.0f;


	void Awake(){
		theTransform = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
		FireAmmo ();
	}


	public void FireAmmo(){
		GameObject ammo = Instantiate (ammoPrefab, theTransform.position, theTransform.rotation) as GameObject;

		Ammo ammoComponent = ammo.GetComponent<Ammo> ();
		Movement movementComponent = ammo.GetComponent<Movement> ();


		ammoComponent.damage = ammoDamage;
		ammoComponent.lifetime = ammoLifetime;

		movementComponent.speed = ammoSpeed;

		Invoke("FireAmmo", Random.Range(timeDelayRange.x, timeDelayRange.y));


	}

	// Update is called once per frame
	void Update () {
	
	}
}
