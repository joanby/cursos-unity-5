using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum ItemType {
	ClickPower,
	PerSecondPower
}

public class ShopItem : MonoBehaviour {

	[Tooltip("Tipo de ítem de la tienda")]
	public ItemType itemType;

	[Tooltip("Nombre del producto")]
	public string name;

	[Tooltip("Coste en euros en la tienda de esta mejora")]
	public int cost;

	[Tooltip("Incremento en euros que genera el botón")]
	public float increaseAmount;

	[Tooltip("Incremento del precio de la mejora")]
	public int increaseCost;

	private int quantity;

	public Text nameText;
	public Text costText;
	public Text quantityText;


	private GameController controller;

	private Button button;

	// Use this for initialization
	void Start () {
		quantity = 0;
		quantityText.text = quantity.ToString ();

		nameText.text = name;
		costText.text = cost.ToString () + " €";

		button = transform.GetComponent<Button> ();
		button.onClick.AddListener (this.ButtonClicked);

		controller = GameObject.FindGameObjectWithTag ("GameController").GetComponent<GameController>();
	}


	void Update(){
		button.interactable = (controller.Money >= cost);
	}
	

	void ButtonClicked(){
		controller.Money -= cost;
		switch (itemType) {
		case ItemType.ClickPower:

			controller.MoneyPerClick += increaseAmount;

			break;
		case ItemType.PerSecondPower:

			controller.MoneyPerSecond += increaseAmount;

			break;
		}

		quantity++;
		quantityText.text = quantity.ToString ();

		cost += increaseCost;
		costText.text = cost.ToString () + " €";

	}


}
