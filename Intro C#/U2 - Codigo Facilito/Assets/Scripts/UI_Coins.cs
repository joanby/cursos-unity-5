using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.UI;

public class UI_Coins : MonoBehaviour {

	private static Text CoinsText;

	private static int TargetCoins = 0;

	// Use this for initialization
	void Start () {
		
		CoinsText = GetComponent<Text> ();
		TargetCoins = Coin.CoinsCount;

		UpdateCoins ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public static void UpdateCoins(){

		int RemainingCoins = Coin.CoinsCount;

		int CollectedCoins = TargetCoins - RemainingCoins;

		if (RemainingCoins > 0) {
			CoinsText.text = "Monedas recogidas\n <color='red'>" + CollectedCoins + "/" + TargetCoins + "</color>";

		
		} else {
			CoinsText.text = "<color='yellow'>Enhorabuena, has recogido todas las monedas</color>";
		}
	}
}

