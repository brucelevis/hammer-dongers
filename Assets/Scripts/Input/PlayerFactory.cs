using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class PlayerFactory : MonoBehaviour {
	const int maxPlayers = 4;
	public List<GameObject> playerPrefabs = new List<GameObject>() {};
	public List<Vector3> playerPositions = new List<Vector3>() {};

	List<PlayerInput> players = new List<PlayerInput>( maxPlayers );
	PlayerActions keyboardListenerPlayerOne;
	PlayerActions keyboardListenerPlayerTwo;
	PlayerActions joystickListener;

	void OnEnable()
	{
		keyboardListenerPlayerOne = PlayerActions.CreateWithKeyboardBindingsForPlayerOne();
		keyboardListenerPlayerTwo = PlayerActions.CreateWithKeyboardBindingsForPlayerTwo();
		joystickListener = PlayerActions.CreateWithJoystickBindings();
	
		Debug.Log (InputManager.Devices.Count + " Devices Found");
		CreatePlayer (null, keyboardListenerPlayerOne);
		CreatePlayer (null, keyboardListenerPlayerTwo);
	}

	void OnDisable()
	{
		joystickListener.Destroy();
		keyboardListenerPlayerOne.Destroy();
		keyboardListenerPlayerTwo.Destroy();
	}
		
	PlayerInput CreatePlayer( InputDevice inputDevice, PlayerActions actions )
	{
		if (players.Count < maxPlayers)
		{
			var playerPosition = playerPositions[players.Count];
			var gameObject = (GameObject) Instantiate( playerPrefabs[players.Count], playerPosition, Quaternion.identity );
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
}
