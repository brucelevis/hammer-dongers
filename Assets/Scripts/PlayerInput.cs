using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class PlayerInput : MonoBehaviour {
	float magnitudeX = 0;
	float magnitudeY = 0;

	float lastDirectionX = 0;
	float lastDirectionY = -1;

	public int playerPrefix = 1;
	PlayerBehaviour player;

	void Start () {
		player = GetComponent<PlayerBehaviour> ();
	}
	
	void Update () {
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

	float GetAxis (string type, string axis)
	{
		float axisValue = Input.GetAxisRaw(type + "P"  + playerPrefix + " " + axis);
		return axisValue;


	}

	float GetAxis (string axis)
	{
		float joystick = GetAxis ("J", axis);
		float keyboard = GetAxis ("K", axis);

		if (joystick == 0 && keyboard == 0)
			return 0;
		return keyboard != 0 ? keyboard : joystick;
	}

	private void InterpretInput () {
		if (Input.GetButtonDown("AttackP" + playerPrefix)) {
			player.Hit ();
			magnitudeY = 0;
			magnitudeX = 0;
			return;
		}

		magnitudeY = GetAxis ("Vertical");
		magnitudeX = GetAxis ("Horizontal");

		if (Input.GetButtonDown ("DashP" + playerPrefix)) {
			player.StartDash (lastDirectionX, lastDirectionY);
		}
	}
}
