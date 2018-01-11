using System.Collections;
using System.Collections.Generic; //Listas
using UnityEngine;
using UnityEngine.UI;

public class PhoneBehaviour : MonoBehaviour {

	public List<GameObject> phoneObjects;

	private bool cameraActive = false;

	public Image cameraFlash;

	private bool shotStarted = false;


	// Use this for initialization
	void Start () {
		SetCameraActive (false);
	}
	
	// Update is called once per frame
	void Update () {

		if ((Input.GetMouseButton (1) || Input.GetAxis("360 Left Trigger")>0) && !this.cameraActive) {
			SetCameraActive (true);
		} else if (this.cameraActive && !(Input.GetMouseButton (1) || Input.GetAxis("360 Left Trigger")>0)) {
			SetCameraActive (false);
		}


		if (this.cameraActive && (Input.GetMouseButton (0) || Input.GetAxis("360 Right Trigger")>0)) {

			this.shotStarted = true;

			StartCoroutine (CameraFlash ());

			GameObject[] enemyList = GameObject.FindGameObjectsWithTag ("Enemy");

			foreach (GameObject enemy in enemyList) {

				if (enemy.activeInHierarchy) {
					EnemyBehaviour behaviour = enemy.GetComponent<EnemyBehaviour> ();
					behaviour.TakeDamage ();
				}

			}

		}

		if (Input.GetAxis ("360 Right Trigger") == 0) {
			this.shotStarted = false;
		}

	}

	void SetCameraActive(bool active){
		
		this.cameraActive = active;

		foreach (GameObject obj in phoneObjects) {
			obj.SetActive (active);
		}

	}


	IEnumerator Fade(float start, float end, float length, Image currentObject){

		/*Comprobamos si el objeto a
		 * modificar tiene por transparencia
		 * el valor de inicio*/
		if (currentObject.color.a == start) {
			Color currentColor; /*Color actual de la imagen*/
			//TRUCO NINJA!!!
			//For que dura exactamente length segundos.
			for (float i = 0.0f; i < 1.0f; i += Time.deltaTime * (1 / length)) {
				//Creamos una copia del color actual
				//No se puede modificar directamente
				currentColor = currentObject.color; 
				//Interpolamos linealmente entre start y end
				//el vlaor actual de la transparencia según lo que indique
				//el parámetro actual 'i'.
				currentColor.a = Mathf.Lerp (start, end, i); // i in [0,1]
				//Copio de vuelta el color final
				currentObject.color = currentColor;
				//Pongo la corutina a esperar hasta que le toque
				yield return null;
			}

			currentColor = currentObject.color;

			/*Nos aseguramos de que la transparencia llega al valor final
			 * y que no ha habido errores posibles de redondeo.
			 */
			currentColor.a = end;
			currentObject.color = currentColor;

		}

	}


	IEnumerator CameraFlash(){
		yield return StartCoroutine (Fade (0.0f, 0.8f, 0.2f, cameraFlash));
		yield return StartCoroutine (Fade (0.8f, 0.0f, 0.2f, cameraFlash));
		StopCoroutine ("CameraFlash");
	}





}
