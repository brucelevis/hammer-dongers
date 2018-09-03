using InControl;
using UnityEngine;

public class CharacterSelectActions : PlayerActionSet
{
	public PlayerAction Join;
	public PlayerAction Leave;
	public PlayerAction Start;
	public PlayerAction ChangeLeft;
	public PlayerAction ChangeRight;

	public CharacterSelectActions()
	{
		Join = CreatePlayerAction( "Join" );
		Leave = CreatePlayerAction( "Leave " );
		Start = CreatePlayerAction( "Start" );
		ChangeLeft = CreatePlayerAction( "ChangeLeft " );
		ChangeRight = CreatePlayerAction( "ChangeRight " );
	}


	public static CharacterSelectActions CreateWithKeyboardBindingsToJoin()
	{
		var actions = new CharacterSelectActions();

		actions.Join.AddDefaultBinding( Key.Space );
		actions.Start.AddDefaultBinding( Key.Return );
		return actions;
	}

	public static CharacterSelectActions CreateWithKeyboardBindingsToSelectCharacter(Key left, Key right, Key leave)
	{
		var actions = new CharacterSelectActions();
		actions.ChangeLeft.AddDefaultBinding( left );
		actions.ChangeRight.AddDefaultBinding( right );
		actions.Leave.AddDefaultBinding( leave );
		return actions;
	}

	public static CharacterSelectActions CreateWithJoystickBindings()
	{
		var actions = new CharacterSelectActions();

		actions.Join.AddDefaultBinding( InputControlType.Action1 );
		actions.Leave.AddDefaultBinding( InputControlType.Action2 );
		actions.ChangeLeft.AddDefaultBinding( InputControlType.LeftBumper );
		actions.ChangeRight.AddDefaultBinding( InputControlType.RightBumper );
		actions.Start.AddDefaultBinding( InputControlType.Start);
		return actions;
	}
}


