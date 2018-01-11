using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {


	public GameObject deathParticlesPrefab = null;
	private Transform theTransform = null;
	public bool shouldBeDestroyedOnDeath = true;
	public bool shouldShowGameOverOnDeath = false;

	public float healthPoints{
		get {
			return _healthPoints;
		}

		set {
			_healthPoints = value;

			if (_healthPoints <= 0) {
				SendMessage ("Die", SendMessageOptions.DontRequireReceiver);

				if (deathParticlesPrefab != null) {
					Instantiate (deathParticlesPrefab, theTransform.position, theTransform.rotation);
					if (shouldBeDestroyedOnDeath) {
						Destroy (gameObject);
					}
				}

				if (shouldShowGameOverOnDeath) {
					GameController.GameOver ();
				}
			}
				
		}
	}


	// Use this for initialization
	void Start () {
		theTransform = GetComponent<Transform> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.Space)) {
			healthPoints = 0.0f;
		}

	}


	[SerializeField]
	private float _healthPoints = 100.0f;

}
