using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyTools.SoundManager;

public class HammerCollision : MonoBehaviour {

	private GridBehaviour grid;
	public AudioClip hitSFX;
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
			SoundManager.PlaySound (hitSFX, 0.75f);
			var tile = collider.GetComponent<TileBehaviour> ();

			tile.Crack ();
			grid.Crack (tile);
		}

		if (collider.gameObject.tag == "Player") {
			var player = collider.GetComponentInParent<PlayerBehaviour> ();
			if (player.animator.GetBool ("Dashing"))
				return;
			player.StartStun ();
		}
	}
}
