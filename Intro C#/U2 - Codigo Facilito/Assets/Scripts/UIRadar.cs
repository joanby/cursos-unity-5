using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UIRadar : MonoBehaviour {

	public float InsideRadarDistance = 20; //Metros del mundo real presentes en el minimapa
	public float CoinSizePercentage = 5; //Tamaño de las monedas en relación al porcentaje del minimapa

	public GameObject CoinImageGold;//Prefab del minimapa de monedas doradas
	public GameObject CoinImageSilver;//Prefab del minimapa de monedas de plata
	public GameObject CoinImageBronze;//Prefab del minimapa de monedas de bronce

	private RawImage RawImageRadarBackground;//Minimapa en si, que es una imagen de textura

	private Transform PlayerTransform;//Posición (transform) del jugador

	private float RadarWidth;//Ancho del radar en px
	private float RadarHeight;//Alto del radar en px

	private float CoinWidth;//Ancho de la miniatura de la moneda
	private float CoinHeight;//Alto de la miniatura de la moneda


	// Use this for initialization
	void Start () {
		PlayerTransform = GameObject.FindGameObjectWithTag ("Player").transform;
		RawImageRadarBackground = GetComponent<RawImage> ();

		RadarWidth = RawImageRadarBackground.rectTransform.rect.width;
		RadarHeight = RawImageRadarBackground.rectTransform.rect.height;

		CoinWidth = RadarWidth * CoinSizePercentage / 100;
		CoinHeight = RadarHeight * CoinSizePercentage / 100;
	}
	
	// Update is called once per frame
	void Update () {

		RemoveAllObjectsFromMinimap ();
		FindAndDisplayObjectsForTag ("Coin");

	}

	//Muestra los objetos en el minimapa con la etiqueta dada
	private void FindAndDisplayObjectsForTag(string tag){
		
		Vector3 PlayerPosition = PlayerTransform.position;
		GameObject[] AllTheCoins = GameObject.FindGameObjectsWithTag (tag);

		foreach (GameObject CoinGO in AllTheCoins) {
			Vector3 CoinPosition = CoinGO.transform.position;
			float DistanceToCoin = Vector3.Distance (CoinPosition, PlayerPosition);

			if (DistanceToCoin <= InsideRadarDistance) {
				Vector3 NormalisedCoinPosition = NormalisedPosition (PlayerPosition, CoinPosition);
				Vector2 CoinMinimapPosition = CalculateObjectPosition (NormalisedCoinPosition);

				Coin c = CoinGO.GetComponent<Coin>();

				GameObject prefab = null;
				if (c.Type == Coin.CoinType.BRONZE) {
					prefab = CoinImageBronze;
				} else if (c.Type == Coin.CoinType.SILVER) {
					prefab = CoinImageSilver;
				} else {
					prefab = CoinImageGold;
				}
				DrawObjectInMinimap (CoinMinimapPosition, prefab);
			}

		}
	}

	//Elimina todos los objetos del minimapa
	private void RemoveAllObjectsFromMinimap(){
		GameObject[] AllTheCoins = GameObject.FindGameObjectsWithTag ("Minimap");
		foreach (GameObject CoinGO in AllTheCoins) {
			Destroy (CoinGO);
		}
	}

	//Nos devuelve el vector que sale de la posición del jugador y apunta donde está la moneda, normalizado al tamaño del minimapa
	private Vector3 NormalisedPosition(Vector3 PlayerPos, Vector3 TargetPos){

		float NormalisedXPosition = (TargetPos.x - PlayerPos.x) / InsideRadarDistance;
		float NormalisedZPosition = (TargetPos.z - PlayerPos.z) / InsideRadarDistance;


		return new Vector3 (NormalisedXPosition, 0, NormalisedZPosition);
	}

	//Dado un objeto en el mundo 3D, qué coordenadas del mundo 2D le tocan
	private Vector2 CalculateObjectPosition(Vector3 TargetPos){

		//parte 1: ángulo que forman el vector del jugador y el de la moneda
		float AngleTarget = Mathf.Atan2 (TargetPos.x, TargetPos.z) * Mathf.Rad2Deg;

		float AnglePlayer = PlayerTransform.eulerAngles.y;

		float AngleRadarDegrees = AngleTarget - AnglePlayer-90;
		float AngleRadarRad = AngleRadarDegrees * Mathf.Deg2Rad;

		//Calcular las coordenadas x e y del vector en el minimapa
		float NormalisedDistanceFromPlayerToTarget = TargetPos.magnitude;

		float MinimapX = NormalisedDistanceFromPlayerToTarget * Mathf.Cos (AngleRadarRad);
		float MinimapY = NormalisedDistanceFromPlayerToTarget * Mathf.Sin (AngleRadarRad);

		MinimapX *= RadarWidth / 2;
		MinimapY *= RadarHeight / 2;

		MinimapX += RadarWidth / 2;
		MinimapY += RadarHeight / 2;

		return new Vector2 (MinimapX, MinimapY);
	}

	//Pinta el Prefab en la posición indicada
	private void DrawObjectInMinimap(Vector2 Pos, GameObject ObjectPrefab){
		GameObject MinimapGO = (GameObject)Instantiate (ObjectPrefab);

		MinimapGO.transform.SetParent (transform.parent);

		RectTransform Rt = MinimapGO.GetComponent<RectTransform> ();
		Rt.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Left, Pos.x, CoinWidth);
		Rt.SetInsetAndSizeFromParentEdge (RectTransform.Edge.Top, Pos.y, CoinHeight);
	}

}
