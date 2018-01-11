using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.SceneManagement;



public class Coin : MonoBehaviour {

	public enum CoinType { BRONZE, SILVER, GOLD };

	public CoinType Type;

	public Material[] CoinMaterials;

	public static int CoinsCount = 0;

	private int CoinValue = 1;

	private CoinType CoinMaterial {
	
		set{
			Type = value;
			int TypeValue = (int)Type;

			Material Mat = CoinMaterials [TypeValue];

			Renderer Rend = GetComponent<Renderer> ();

			if (Rend != null) {
				Rend.material = Mat;
				CoinValue = 1 + (int) Mathf.Pow (TypeValue, 2);
			}

		}

		get{
			return Type;
		}
	}

	// Se llama cuando empieza el juego
	void Awake () {

		CoinMaterial = Type;

		Coin.CoinsCount += CoinValue;

	}

	//Se llama cuando se destruye un objeto
	void OnDestroy(){
		if (!UICountdown.IsGameFinished) {
			UICountdown.TimerBonus = 3 * CoinValue * CoinValue;
		}
			Coin.CoinsCount -= CoinValue;

			UI_Coins.UpdateCoins ();

		if (Coin.CoinsCount <= 0&&!UICountdown.IsGameFinished) {
			Debug.Log ("He recogido todas las monedas. Game Over");

			UICountdown.IsGameFinished = true;
			int SceneId = SceneManager.GetActiveScene().buildIndex;
			SceneManager.LoadScene (SceneId, LoadSceneMode.Single);
		}
		

	}


	void OnTriggerEnter(Collider OtherCollider){
	
		if (OtherCollider.CompareTag ("Player")) {
			Destroy (gameObject);
		}

	}

}