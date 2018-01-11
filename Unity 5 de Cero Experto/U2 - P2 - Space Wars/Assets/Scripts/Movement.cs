using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private Transform theTransform = null;
	public float maxSpeed = 8.0f;


	void Awake() {
		theTransform = GetComponent<Transform> ();
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		theTransform.position += theTransform.forward * maxSpeed * Time.deltaTime;
	}
}
