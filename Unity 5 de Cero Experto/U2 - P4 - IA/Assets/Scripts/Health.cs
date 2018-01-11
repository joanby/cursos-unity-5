using UnityEngine;
using System.Collections;

public class Health : MonoBehaviour {

	public float HealthPoints{
		get{ 
			return healthPoints; 
		}

		set {
			healthPoints = value;

			if (healthPoints <= 0) {
				Destroy (gameObject);
			}

		}

	}

	[SerializeField]
	private float healthPoints = 100.0f;

}
