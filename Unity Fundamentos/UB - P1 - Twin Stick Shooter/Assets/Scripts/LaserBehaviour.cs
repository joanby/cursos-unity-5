using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserBehaviour : MonoBehaviour {

	/*Variables*/
	public float lifetime = 2.0f;//tiempo de vida del laser

	public float speed = 5.0f; // velocidad del laser

	public int damage = 1;

	// Use this for initialization
	void Start () {
		Destroy (gameObject, lifetime);		
	}
	
	// Update is called once per frame
	void Update () {
		//s = v * t
		this.transform.Translate(Vector3.up*speed*Time.deltaTime);

	}
}
