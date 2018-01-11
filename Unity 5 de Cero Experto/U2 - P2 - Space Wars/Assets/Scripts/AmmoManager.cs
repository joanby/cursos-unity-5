using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmmoManager : MonoBehaviour {

	public GameObject ammoPrefab = null;

	public int poolSize = 100;

	public Queue<Transform> ammoQueue = new Queue<Transform> ();

	private GameObject[] ammoArray;

	public static AmmoManager ammoManager = null;



	void Awake(){
		if (ammoManager != null) {
			Destroy (GetComponent<AmmoManager> ());
			return;
		}

		ammoManager = this;

		ammoArray = new GameObject[poolSize];

		for (int i = 0; i < poolSize; i++) {
			GameObject ammo = Instantiate(ammoPrefab, Vector3.zero, Quaternion.identity) as GameObject;
			Transform objectTransform = ammo.GetComponent<Transform> ();
			objectTransform.parent = GetComponent<Transform> ();

			ammoQueue.Enqueue (objectTransform);
			ammo.SetActive (false);
			ammoArray [i] = ammo;
		
		}
	
	} 


	public static Transform SpawnAmmo(Vector3 position, Quaternion rotation){
	
		Transform ammo = ammoManager.ammoQueue.Dequeue ();

		ammo.gameObject.SetActive (true);
		ammo.position = position;
		ammo.localRotation = rotation;

		ammoManager.ammoQueue.Enqueue (ammo);

		return ammo;
	
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
