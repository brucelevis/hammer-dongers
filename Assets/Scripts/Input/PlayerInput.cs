using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour {
	float magnitudeX = 0;
	float magnitudeY = 0;

	float lastDirectionX = 0;
	float lastDirectionY = -1;

	public int PlayerPrefix   = 1;
	PlayerBehaviour player;
	public PlayerActions Actions { get; set; }

	void Start () {
		player = GetComponent<PlayerBehaviour> ();
	}
	
	void Update () {
		if (Actions == null)
			return;
		
		if(Time.timeSinceLevelLoad > 1)
			InterpretInput();
	}
		
	void FixedUpdate () {
		if (magnitudeX != 0 || magnitudeY != 0) {
			lastDirectionX = magnitudeX;
			lastDirectionY = magnitudeY;
		}
		player.Move (new Vector2(magnitudeX, magnitudeY));
	}

	private void InterpretInput () {
		if (Actions.Smash.WasPressed) {
			player.Hit ();
			magnitudeY = 0;
			magnitudeX = 0;
			return;
		}

		magnitudeY = Actions.Move.Y;
		magnitudeX = Actions.Move.X;

		if (Actions.Dash.WasPressed) 
			player.StartDash (lastDirectionX, lastDirectionY);
	}
}
