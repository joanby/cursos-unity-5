using UnityEngine;
using System.Collections;

public class CollissionDamage : MonoBehaviour {

	public float damagePoints = 10.0f;


	void OnTriggerStay(Collider otherCollider){


		Health health = otherCollider.gameObject.GetComponent<Health> ();

		Debug.Log (health);

		if (health == null)
			return;

		health.healthPoints -= damagePoints * Time.deltaTime;

	}


	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
