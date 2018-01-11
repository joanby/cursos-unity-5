using UnityEngine;
using System.Collections;

public class Movement : MonoBehaviour {

	private Transform theTransform = null;
	public float speed = 10.0f;

	void Awake(){
		theTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		theTransform.position += theTransform.forward * speed * Time.deltaTime;
	}
}
