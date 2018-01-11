using UnityEngine;
using System.Collections;

public class DestroyAfterTime : MonoBehaviour {

	public float destroyTime = 2.0f;

	// Use this for initialization
	void Start () {
		Invoke ("Die", destroyTime);
	}

	void Die(){
		Destroy (gameObject);
	}

	// Update is called once per frame
	void Update () {
	
	}
}
