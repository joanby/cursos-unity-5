using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleBehaviour : RepeatBackground {


	protected override void Offscreen (ref Vector3 pos)
	{
		Destroy (this.gameObject);
	}


	public void OnTriggerEnter2D(Collider2D collider){
		GameController.Score++;
	}

}
