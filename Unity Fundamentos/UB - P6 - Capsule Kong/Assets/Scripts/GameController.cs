using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	private ParticleSystem exitPS;
	public ParticleSystem ExitPS{

		get{
			return exitPS;
		}

		set{
			exitPS = value;
		}

	}


	public static GameController sharedInstance;
	private int orbsCollected;
	private int orbsTotal;

	public Text orbsText;

	void Awake(){
		sharedInstance = this;
	}

	// Use this for initialization
	void Start () {
		
		GameObject[] orbs = GameObject.FindGameObjectsWithTag ("Orb");
		orbsCollected = 0;
		orbsTotal = orbs.Length;

		orbsText.text = "Orbes: " + orbsCollected + " de " + orbsTotal;

	}

	void Update(){

		if (Input.GetKeyDown (KeyCode.F2)) {
			UpdateOrbTotal (true);
			this.gameObject.GetComponent<LevelEditor> ().enabled = true;
		}
	}

	/*void BuildLevel(){
		//Buscamos el padre para añadir los hijos en forma de muros
		GameObject dynamicParent = GameObject.Find ("Dynamic Objects");
		//Recorremos nuestro array bidimensional level para construir el nivel
		for (int idy = 0; idy < level.Length; idy++) {
			for (int idx = 0; idx < level [idy].Length; idx++) {

				Transform objectToCreate = null;

				//Si el valor es 1, construye el muro
				switch (level [idy] [idx]) {

				//No hay que hacer nada
				case 0:
					break;
				//Crear el muro
				case 1:
					objectToCreate = wall;
					break;
				//Crear el personaje
				case 2:
					objectToCreate = player;
					break;
				//Crear un orbe
				case 3:
					objectToCreate = orb;
					break;
				//Crear el portal de salida
				case 4:
					objectToCreate = exit;
					break;
				}

				if (objectToCreate != null) {
					Vector3 pos = new Vector3 (idx, level.Length - idy, 0);
					Transform newObject = Instantiate (objectToCreate, pos, wall.rotation) as Transform;
					newObject.parent = dynamicParent.transform;
					if (objectToCreate == exit) {
						exitPS = newObject.GetComponent<ParticleSystem> ();
					}
				}
			}
		}
	}*/




	public void CollectOrb(){
		orbsCollected++;
		orbsText.text = "Orbes: " + orbsCollected + " de " + orbsTotal;

		if (orbsCollected >= orbsTotal) {
			exitPS.Play ();
		}
	}

	public void UpdateOrbTotal(bool reset = false){

		if (reset) {
			orbsCollected = 0;
		}

		GameObject[] orbs = GameObject.FindGameObjectsWithTag ("Orb");

		orbsTotal = orbs.Length;

		orbsText.text = "Orbes: " + orbsCollected + " de " + orbsTotal;

	}

}
