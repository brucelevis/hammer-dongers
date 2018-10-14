using System.Collections.Generic;
using InControl;
using UnityEngine;
using UnityEngine.SceneManagement;


public class CharacterSelectInputBehaviour : MonoBehaviour
{
	List<PlayerInputConfiguration> players;
	PlayerConfigurationContainer container;

	CharacterSelectActions joinKeyboardListener;
	CharacterSelectActions selectKeyboardListenerP1;
	CharacterSelectActions selectKeyboardListenerP2;
	CharacterSelectActions joystickListener;

	PlayerActions keyboardP1;
	PlayerActions keyboardP2;
	PlayerJoin playerJoin;

	void OnEnable()
	{
		container = PlayerConfigurationContainer.getInstance ();
		players = container.Configurations;
		players.Clear ();

		InputManager.OnDeviceDetached += OnDeviceDetached;
		playerJoin = GetComponent<PlayerJoin> ();

		selectKeyboardListenerP1 = CharacterSelectActions.CreateWithKeyboardBindingsToSelectCharacter(Key.A, Key.D, Key.LeftShift);
		selectKeyboardListenerP2 = CharacterSelectActions.CreateWithKeyboardBindingsToSelectCharacter(Key.LeftArrow, Key.RightArrow, Key.RightShift);
		joinKeyboardListener = CharacterSelectActions.CreateWithKeyboardBindingsToJoin();

		joystickListener = CharacterSelectActions .CreateWithJoystickBindings();

		keyboardP1 = PlayerActions.CreateWithKeyboardBindingsForPlayerOne ();
		keyboardP2 = PlayerActions.CreateWithKeyboardBindingsForPlayerTwo ();
	}


	void OnDisable()
	{
		InputManager.OnDeviceDetached -= OnDeviceDetached;
		joinKeyboardListener.Destroy();
		selectKeyboardListenerP1.Destroy();
		selectKeyboardListenerP2.Destroy();
		joystickListener.Destroy();
	}


	void Update()
	{
		if (JoinButtonWasPressedOnListener( joystickListener))
		{
			var inputDevice = InputManager.ActiveDevice;
			if (FindPlayerUsingJoystick( inputDevice ) == null)
			{
				var actions = PlayerActions.CreateWithJoystickBindings ();
				var characterSelectActions = CharacterSelectActions.CreateWithJoystickBindings();

				characterSelectActions.Device = inputDevice;
				actions.Device = inputDevice;
				CreatePlayer( actions, characterSelectActions);
			}
		}

		if (JoinButtonWasPressedOnListener( joinKeyboardListener ))
		{
			var keyboardCount = container.GetKeyboardCount ();
			if (keyboardCount >= 2)
				return;

			PlayerActions actions = keyboardCount == 0 ? keyboardP1 : keyboardP2;
			CharacterSelectActions characterSelectActions = keyboardCount == 0 ? selectKeyboardListenerP1 : selectKeyboardListenerP2;
			CreatePlayer (actions, characterSelectActions);
		}

		if (container.Configurations.Count <= 1)
			return;

		if (joinKeyboardListener.Start.WasPressed || joystickListener.Start.WasPressed) 
			StartGame ();
		
	}

	bool JoinButtonWasPressedOnListener( CharacterSelectActions actions )
	{
		return actions.Join.WasPressed;
	}

	PlayerInputConfiguration FindPlayerUsingJoystick( InputDevice inputDevice )
	{
		var playerCount = players.Count;
		for (var i = 0; i < playerCount; i++)
		{
			var player = players[i];
			if (player.Actions.Device == inputDevice)
				return player;
		}

		return null;
	}

	void OnDeviceDetached( InputDevice inputDevice )
	{
		var player = FindPlayerUsingJoystick( inputDevice );
		if (player != null)
			RemovePlayer( player );
	}

	int GetNextCharacter ()
	{
		return players.Count;
	}

	PlayerInputConfiguration CreatePlayer( PlayerActions actions, CharacterSelectActions characterSelectActions )
	{
		if (players.Count < container.MAX_PLAYERS)
		{
			int index = container.GetAvailablePlayerIndex ();
			PlayerInputConfiguration player = new PlayerInputConfiguration (index, actions, players.Count + 1);
			players.Add( player );

			playerJoin.Join (index, characterSelectActions);
			return player;
		}
		return null;
	}

	void StartGame () {
		SceneManager.LoadScene ("Scene 3");
	}


	void RemovePlayer( PlayerInputConfiguration player )
	{
		players.Remove( player );
		player.Actions = null;
	}
}
