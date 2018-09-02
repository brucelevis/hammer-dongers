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

	void Awake()
	{
		keyboardListenerPlayerOne = PlayerActions.CreateWithKeyboardBindingsForPlayerOne();
		keyboardListenerPlayerTwo = PlayerActions.CreateWithKeyboardBindingsForPlayerTwo();
		joystickListener = PlayerActions.CreateWithJoystickBindings();

		var container = PlayerConfigurationContainer.getInstance ();

		//hardcoded configs
		//joystickListener.Device = InputManager.Devices[0];

		PlayerActions p1a = keyboardListenerPlayerOne;
		//PlayerActions p1a = joystickListener;
		PlayerInputConfiguration p1 = new PlayerInputConfiguration (0, p1a);
		PlayerInputConfiguration p2 = new PlayerInputConfiguration (1, keyboardListenerPlayerTwo);

		container.Configurations.Add (p1);
		container.Configurations.Add (p2);

		foreach (PlayerInputConfiguration config in container.Configurations) 
			CreatePlayer (config);
	}

	void OnDisable()
	{
		joystickListener.Destroy();
		keyboardListenerPlayerOne.Destroy();
		keyboardListenerPlayerTwo.Destroy();
	}

	PlayerInput CreatePlayer( PlayerInputConfiguration configuration )
	{
		if (players.Count < maxPlayers)
		{
			var playerPosition = playerPositions[configuration.PlayerIndex];
			var gameObject = (GameObject) Instantiate( playerPrefabs[configuration.PlayerIndex], playerPosition, Quaternion.identity );
			var player = gameObject.GetComponent<PlayerInput>();

			player.Actions = configuration.Actions;

			players.Add( player );
			gameObject.name = "Player" + players.Count;
			player.PlayerPrefix = players.Count;
			return player;
		}
		return null;
	}
}
