using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

	public Transform enemy;

	[Header("Oleadas de enemigos")]
	public float timeBeforeSpawning = 1.5f;
	public float timeBetweenEnemies = 0.25f;
	public float timeBetweenWaves   = 2.0f; 

	public int enemiesPerWave = 10;
	private int currentNumberOfEnemies = 0;

	[Header("User Interface")]
	private int score = 0;
	private int wave  = 0;

	public Text scoreText;
	public Text waveText;

	// Use this for initialization
	void Start () {
		StartCoroutine (SpawnEnemies ());	
	}


	//Indica una corutina en Unity
	IEnumerator SpawnEnemies(){

		//Indicamos que queremos esperar 1.5 segundos antes de arrancar el juego
		yield return new WaitForSeconds (timeBeforeSpawning);

		//Después de ese tiempo de espera inicial, arrancamos el bucle de las oleadas
		while (true) {

			//No crees nada hasta que la oleada de enemigos previa haya sido eliminada
			if (currentNumberOfEnemies <= 0) {
				IncreaseWave ();
				//No quedan enemigos, hay que crear unos 10 nuevos enemigos
				for (int i = 0; i < enemiesPerWave; i++) {
					//Generamos aleatoriamente el enemigo fuera de la pantalla
					float randDistance = Random.Range(10, 25); //Distancia de aparición
					Vector2 randDirection = Random.insideUnitCircle; //Punto al azar sobre la circumferencia de radio 1
					//Posición donde generar al enemigo
					Vector3 enemyPos = this.transform.position; //Posición del Game Controller (0,0,0)
					enemyPos.x += randDirection.x * randDistance;
					enemyPos.y += randDirection.y * randDistance;

					//Instanciamos el enemigo en la pantalla
					Instantiate(enemy, enemyPos, this.transform.rotation);
					//Indicamos que hay un nuevo enemigo en pantalla
					currentNumberOfEnemies++;
					//Indicamos a la corutina que duerma durante 0.25 segundos
					yield return new WaitForSeconds(timeBetweenEnemies);
				}

			}

			//Si llego aquí, es que aún tengo enemigos, entonces, le indico al bucle principal
			//que espere otros 2 segundos más
			yield return new WaitForSeconds(timeBetweenWaves);
		}
		
	}



	public void KillEnemy(){
		currentNumberOfEnemies--;
	}

	public void IncreaseScore(int increaseScore){
		this.score += increaseScore; // score = score + increaseScore
		this.scoreText.text = "Score : "+this.score;
	}

	private void IncreaseWave(){
		this.wave++;
		this.waveText.text = "Wave : " + this.wave;
	}



}
