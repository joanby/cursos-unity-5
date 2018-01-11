using UnityEngine;
using System.Collections;
using System.Collections.Generic;



public class LevelGenerator : MonoBehaviour {

	/*
	 * Variables públicas de nuestro generador de niveles
	 * 
	*/
	public static LevelGenerator sharedInstance; // instancia compartida para solo tener un generador de niveles
	public List<LevelBlock> allTheLevelBlocks = new List<LevelBlock>(); //Lista que contiene todos los niveles que se han creado
	public List<LevelBlock> currentLevelBlocks = new List<LevelBlock>();//Lista de los bloques que tenemos ahora mismo en pantalla
	public Transform levelInitialPoint; // Punto inicial donde empezará a crearse el primer nivel de todos

	private bool isGeneratingInitialBlocks = false;

	void Awake() {
		sharedInstance = this;
	}



	// Use this for initialization
	void Start () {
		GenerateInitialBlocks ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}


	public void GenerateInitialBlocks(){
		isGeneratingInitialBlocks = true;
		for (int i = 0; i < 3; i++) {
			AddNewBlock ();
		}
		isGeneratingInitialBlocks = false;
	}

	public void AddNewBlock() {
		//seleccionar un bloque aleatorio entre los que tenemos disponibles
		int randomIndex = Random.Range(0, allTheLevelBlocks.Count);

		if (isGeneratingInitialBlocks) {
			randomIndex = 0;
		}

		LevelBlock block = (LevelBlock)Instantiate (allTheLevelBlocks [randomIndex]);
		block.transform.SetParent (this.transform, false);

		//POsición del bloque
		Vector3 blockPosition = Vector3.zero;

		if (currentLevelBlocks.Count == 0) {
			//Vamos a colocar el primer bloque en pantalla.
			blockPosition = levelInitialPoint.position;
		} else {
			//Ya tengo bloques en pantalla, lo empalmo al último disponible
			blockPosition = currentLevelBlocks [currentLevelBlocks.Count - 1].exitPoint.position;
		}

		block.transform.position = blockPosition;
		currentLevelBlocks.Add (block);

	}

	public void RemoveOldBlock() {
		LevelBlock block = currentLevelBlocks [0];
		currentLevelBlocks.Remove (block);
		Destroy (block.gameObject);
	
	}

	public void RemoveAllTheBlocks() {
		while (currentLevelBlocks.Count > 0) {
			RemoveOldBlock ();
		}
	}

}
