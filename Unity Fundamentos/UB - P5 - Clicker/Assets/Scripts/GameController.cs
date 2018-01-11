using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {


	[Header("UI object references")]
	public Text moneyText;
	public Text rateText;
	public Text buttonText;

	private float money;//Dinero total del usuario

	public float Money{
		get{
			return money;
		}

		set{
			money = value;
			moneyText.text = "Tienes: " + money.ToString ("0.00") + " €";
		}

	}


	private float moneyPerSecond;//Dinero que generamos automáticamente por segundo

	public float MoneyPerSecond {
		get{
			return moneyPerSecond;
		}

		set{
			moneyPerSecond = value;
			rateText.text = "por segundo: " + moneyPerSecond.ToString ("0.0");
		}
	}

	[Tooltip("Cuanto dinero generará el jugador con cada click del botón")]
	private float moneyPerClick;
	public float MoneyPerClick {
		get{
			return moneyPerClick;
		}

		set{
			moneyPerClick = value;
			buttonText.text = "Click Me!\n" + moneyPerClick.ToString ()+ " €";
		}
	}

	// Use this for initialization
	void Start () {
		Money = 0;
		MoneyPerSecond = 0;
		MoneyPerClick = 1;
	}
	
	// Update is called once per frame
	void Update () {
		Money += MoneyPerSecond * Time.deltaTime;
	}

	public void ButtonClicked(){

		Money += moneyPerClick;
	}
}
