using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerInputDeviceManager : MonoBehaviour
{
	public GameObject playerPrefab;

	const int maxPlayers = 4;

	public List<Vector3> playerPositions = new List<Vector3>() {
		new Vector3( -6, 0, -1 ),
		new Vector3( 6, 1, -1 ),
	};

	List<PlayerInput> players = new List<PlayerInput>( maxPlayers );

	PlayerActions keyboardListenerPlayerOne;
	PlayerActions keyboardListenerPlayerTwo;
	PlayerActions joystickListener;

	void OnEnable()
	{
		InputManager.OnDeviceDetached += OnDeviceDetached;
		keyboardListenerPlayerOne = PlayerActions.CreateWithKeyboardBindingsForPlayerOne();
		keyboardListenerPlayerTwo = PlayerActions.CreateWithKeyboardBindingsForPlayerTwo();
		joystickListener = PlayerActions.CreateWithJoystickBindings();
	}


	void OnDisable()
	{
		InputManager.OnDeviceDetached -= OnDeviceDetached;
		joystickListener.Destroy();
		keyboardListenerPlayerOne.Destroy();
		keyboardListenerPlayerTwo.Destroy();
	}


	void Update()
	{

	}


	bool JoinButtonWasPressedOnListener( PlayerActions actions )
	{
		return actions.Smash.WasPressed || actions.Dash.WasPressed || actions.Move.WasPressed;
	}


	PlayerInput FindPlayerUsingJoystick( InputDevice inputDevice )
	{
		var playerCount = players.Count;
		for (var i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.Actions.Device == inputDevice)
			{
				return player;
			}
		}

		return null;
	}


	bool ThereIsNoPlayerUsingJoystick( InputDevice inputDevice )
	{
		return FindPlayerUsingJoystick( inputDevice ) == null;
	}


	PlayerInput FindPlayerUsingKeyboard()
	{
		var playerCount = players.Count;
		for (var i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.Actions == keyboardListenerPlayerOne)
			{
				return player;
			}
		}

		return null;
	}


	bool ThereIsNoPlayerUsingKeyboard()
	{
		return FindPlayerUsingKeyboard() == null;
	}


	void OnDeviceDetached( InputDevice inputDevice )
	{
		
	}


	PlayerInput CreatePlayer( InputDevice inputDevice, PlayerActions actions )
	{
		if (players.Count < maxPlayers)
		{
			// Pop a position off the list. We'll add it back if the player is removed.
			var playerPosition = playerPositions[0];
			playerPositions.RemoveAt( 0 );

			var gameObject = (GameObject) Instantiate( playerPrefab, playerPosition, Quaternion.identity );

			var player = gameObject.GetComponent<PlayerInput>();

			if(actions == null)
				actions = PlayerActions.CreateWithJoystickBindings();
			
			player.Actions = actions;

			if (inputDevice != null)
				actions.Device = inputDevice;
			
			players.Add( player );
			gameObject.name = "Player" + players.Count;
			player.PlayerPrefix = players.Count;
			return player;
		}

		return null;
	}


	void RemovePlayer( PlayerInput player )
	{
		playerPositions.Insert( 0, player.transform.position );
		players.Remove( player );
		player.Actions = null;
		Destroy( player.gameObject );
	}
}