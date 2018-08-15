using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerCollision : MonoBehaviour {

	private GridBehaviour grid;
	void SetGrid ()
	{
		grid = GameObject.FindGameObjectWithTag ("Grid").GetComponent<GridBehaviour> ();
	}

	void Awake() {
		
		SetGrid ();
	}


	void OnTriggerEnter2D(Collider2D collider) {
		if (grid == null)
			SetGrid ();

		if (collider.gameObject.tag == "Tile") {
			AudioManager.playSFX("hit", 0.4f, true);
			var tile = collider.GetComponent<TileBehaviour> ();

			tile.Crack ();
			grid.Crack (tile);
		}

		if (collider.gameObject.tag == "Player") {
			AudioManager.playSFX("hit", 0.4f, true);
			var player = collider.GetComponentInParent<PlayerBehaviour> ();
			if (!player.animator.GetBool ("Dashing"))
				player.StartStun ();
		}
	}
}
