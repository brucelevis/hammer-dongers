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
			var playerPosition = playerPositions[configuration.PlayerColorIndex];
			var gameObject = (GameObject) Instantiate( playerPrefabs[configuration.PlayerColorIndex], playerPosition, Quaternion.identity );

			var swap = gameObject.GetComponent<SwapTexture> ();
			swap.swapColors = CharacterColors.GetPalette (configuration.PlayerColorIndex);

			var player = gameObject.GetComponent<PlayerInput>();

			player.Actions = configuration.Actions;

			players.Add( player );
			gameObject.name = "Player" + configuration.PlayerSuffix;
			player.PlayerSuffix = configuration.PlayerSuffix;
			return player;
		}
		return null;
	}
}
