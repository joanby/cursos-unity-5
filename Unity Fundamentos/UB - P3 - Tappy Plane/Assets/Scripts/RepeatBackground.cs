using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatBackground : MonoBehaviour {


	[Tooltip("Cómo debe moverse el fondo de rápido")]
	public float scrollSpeed;

	//<summary>
	//Cómo de grande es la imagen de fondo
	//</summary>
	public const float ScrollWidth = 8;


	//<summary>
	//Se llama a un intervalo de tiempo fijo, en nuestro caso
	//para mover el fondo a intervalos regulares
	//</summary>
	private void FixedUpdate(){

		//Posición actual del fondo
		Vector3 pos = this.transform.position;

		//Desplazamos el fondo en sentido negativo 
		//de las x usando s = v * t 
		pos.x -= this.scrollSpeed * Time.deltaTime * GameController.speedModifier;

		//Si el gráfico se ha apartado totalmente de la pantalla
		//procedemos a destruirlo
		if (this.transform.position.x < -ScrollWidth) {
			Offscreen (ref pos);
		}


		//Si no hemos destruido el fondo, reposiciono la nueva posición
		this.transform.position = pos;
	}

	//<summary>
	//Se llama al método cuando el fondo ha sobrepasado toda la pantalla.
	//</summary>
	//<param name = "pos">Referencia la posición del objeto </param>
	protected virtual void Offscreen(ref Vector3 pos){
		pos.x += 2 * ScrollWidth;
	}

}
