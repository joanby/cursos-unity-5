using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;

public class LevelEditor : MonoBehaviour {


	public List<Transform> tiles;

	int xMin = 0;
	int xMax = 0;
	int yMin = 0;
	int yMax = 0;

	GameObject dynamicParent;

	private int[][] level = new int[][] {
		new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 3, 0, 4, 1 },
		new int[] { 1, 1, 1, 0, 0, 0, 0, 0, 3, 0, 0, 0, 1, 1, 1, 1, 1, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 1, 1, 1, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 3, 0, 0, 0, 0, 0, 0, 1, 0, 0, 1, 0, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 1, 1, 1, 1, 0, 0, 0, 0, 1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 1 },
		new int[] { 1, 1, 1, 1, 1, 1, 0, 0, 0, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 2, 0, 0, 1, 1, 1, 3, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 0, 0, 0, 0, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 0, 0, 3, 0, 0, 0, 0, 0, 0, 0, 0, 3, 0, 0, 0, 0, 0, 1 },
		new int[] { 1, 0, 0, 1, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 3, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 0, 3, 1, 0, 0, 0, 0, 0, 0, 0, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 0, 0, 0, 1, 1, 1, 1 },
		new int[] { 1, 0, 0, 0, 0, 0, 0, 1, 1, 1, 1, 0, 0, 3, 0, 1, 1, 1, 1, 1 },
		new int[] { 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1, 1 }
	};

	private string levelName = "Level1";

	private Transform tileToCreate;

	// Use this for initialization
	void Start () {
		dynamicParent = GameObject.Find ("Dynamic Objects");
		if (dynamicParent == null) {
			print ("Objeto no encontrado! Asegúrate de que lo has escrito bien!");
		}
		BuildLevel ();
		enabled = false;

		tileToCreate = tiles [0];

		GameController.sharedInstance.UpdateOrbTotal (true);

	}
	
	void BuildLevel(){
		for (int yPos = 0; yPos < level.Length; yPos++) {
			for (int xPos = 0; xPos < level [yPos].Length; xPos++) {
				CreateBlock (level [yPos] [xPos], xPos, level.Length-yPos);
			}
		}
	}


	public void CreateBlock(int value, int xPos, int yPos){

		Transform objectToCreate = null;

		//Necesitamos saber el tamaño del nivel para guardar en fichero
		if (xPos < xMin) {
			xMin = xPos;
		}
		if (xPos > xMax) {
			xMax = xPos;
		}
		if (yPos < yMin) {
			yMin = yPos;
		}
		if (yPos > yMax) {
			yMax = yPos;
		}

		if (value != 0) {
			objectToCreate = tiles [value - 1];
		}

		if (objectToCreate != null) {
			Transform newObject = Instantiate (objectToCreate, new Vector3 (xPos, yPos, 0), Quaternion.identity) as Transform;
			newObject.name = objectToCreate.name;
			if (objectToCreate.name == "Exit") {
				GameController.sharedInstance.ExitPS = newObject.gameObject.GetComponent<ParticleSystem> ();
				newObject.transform.Rotate (-90, 0, 0);
			}

			newObject.parent = dynamicParent.transform;
		}

	}



	void Update(){

		if (Input.GetMouseButton (0) && GUIUtility.hotControl == 0) {

			Vector3 mousePos = Input.mousePosition;

			mousePos.z = -Camera.main.transform.position.z;

			Vector3 pos = Camera.main.ScreenToWorldPoint (mousePos);

			int posX = Mathf.FloorToInt (pos.x + 0.5f);
			int posY = Mathf.FloorToInt (pos.y + 0.5f);

			Collider[] hitColliders = Physics.OverlapSphere (pos, 0.45f);
			int i = 0;
			while (i < hitColliders.Length) {

				if (tileToCreate.name != hitColliders [i].gameObject.name) {

					DestroyImmediate (hitColliders [i].gameObject);

				} else {
					return;
				}

				i++;

			}

			CreateBlock (tiles.IndexOf (tileToCreate)+1, posX, posY);

			GameController.sharedInstance.UpdateOrbTotal ();
		}



		if (Input.GetMouseButton (1) && GUIUtility.hotControl == 0) {

			Ray ray = Camera.main.ScreenPointToRay (Input.mousePosition);

			RaycastHit hit = new RaycastHit ();

			Physics.Raycast (ray, out hit, 100);

			if (hit.collider != null && hit.collider.name != "Player") {
				Destroy (hit.collider.gameObject);
			}

			GameController.sharedInstance.UpdateOrbTotal ();
		}
	}


	void OnGUI(){

		GUILayout.BeginArea (new Rect (Screen.width - 120, 20, 100, 800));
		foreach (Transform tile in tiles) {
			if (GUILayout.Button (tile.name)) {
				tileToCreate = tile;
			}
		}

		levelName = GUILayout.TextField (levelName);

		if (GUILayout.Button ("Save")) {
			SaveLevel ();
		}


		if (GUILayout.Button ("Load")) {
			LoadLevel ();
		}

		if (GUILayout.Button ("Quit")) {
			enabled = false;
		}
		GUILayout.EndArea ();

	}

	void SaveLevel(){
		List<string> newLevel = new List<string> ();

		for (int i = yMin; i <= yMax; i++) {
			string newRow = "";

			for (int j = xMin; j <= xMax; j++) {

				Vector3 pos = new Vector3 (j, i, 0);
				Ray ray = Camera.main.ScreenPointToRay (pos);
				RaycastHit hit = new RaycastHit ();
				Physics.Raycast (ray, out hit, 100);

				Collider[] hitColliders = Physics.OverlapSphere (pos, 0.1f);

				if (hitColliders.Length > 0) {
					for (int k = 0; k < tiles.Count; k++) {
						if (tiles [k].name == hitColliders [0].gameObject.name) {
							newRow += (k + 1).ToString() + ",";
						}
					}
				} else {
					newRow += "0,";
				}
			}
			newRow += "\n";
			newLevel.Add (newRow);
		}

		newLevel.Reverse ();

		string levelComplete = "";

		foreach (string level in newLevel) {
			levelComplete += level;
		}

		print (levelComplete);

		//Lógica de guardar el fichero
		BinaryFormatter formatter = new BinaryFormatter ();
		FileStream file = File.Create (Application.persistentDataPath + "/" + levelName + ".lvl");
		formatter.Serialize (file, levelComplete);
		file.Close ();
	}

	void LoadLevel(){
		if (File.Exists (Application.persistentDataPath + "/" + levelName + ".lvl")) {
			//Debemos cargar el nivel actual en escena
			foreach (Transform child in dynamicParent.transform) {
				Destroy (child.gameObject);
			}

			//Lógica de leer del fichero
			BinaryFormatter formatter = new BinaryFormatter();
			FileStream file = File.OpenRead (Application.persistentDataPath + "/" + levelName + ".lvl");
			string levelData = formatter.Deserialize (file) as string;
			file.Close ();

			LoadLevelFromString (levelData);

			//levelName = level;

			PlayerStart.isSpawned = false;
			StartCoroutine (LoadUpdated ());
		} else {
			levelName = "Error!!!";
		}
	}

	public void LoadLevelFromString(string levelContent){
		List<string> levelRows = new List<string> (levelContent.Split ('\n'));
		for (int i = 0; i < levelRows.Count; i++) {
			string[] ids = levelRows [i].Split (',');
			for (int j = 0; j < ids.Length - 1; j++) {
				CreateBlock (int.Parse (ids [j]), j, levelRows.Count - i);
			}
		}
	}

	IEnumerator LoadUpdated(){
		yield return 0;
		GameController.sharedInstance.UpdateOrbTotal ();
	}

}
