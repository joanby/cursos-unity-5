using UnityEngine;
using System.Collections;

public class FollowDestination : MonoBehaviour {

	private UnityEngine.AI.NavMeshAgent theAgent = null;
	public Transform destination = null;

	// Use this for initialization
	void Awake () {
		theAgent = GetComponent<UnityEngine.AI.NavMeshAgent> ();
	}
	
	// Update is called once per frame
	void Update () {
		theAgent.SetDestination (destination.position);
	}
}
