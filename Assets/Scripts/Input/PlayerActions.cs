using InControl;

public class PlayerActions : PlayerActionSet
{
	public PlayerAction Left;
	public PlayerAction Right;
	public PlayerAction Up;
	public PlayerAction Down;
	public PlayerAction Smash;
	public PlayerAction Dash;
	public PlayerTwoAxisAction Move;

	public PlayerActions()
	{
		Dash = CreatePlayerAction( "Dash" );
		Smash = CreatePlayerAction( "Smash" );
		Up = CreatePlayerAction( "Up" );
		Down = CreatePlayerAction( "Down" );
		Left = CreatePlayerAction( "Left" );
		Right = CreatePlayerAction( "Right" );
		Move = CreateTwoAxisPlayerAction( Left, Right, Down, Up );
	}


	public static PlayerActions CreateWithKeyboardBindingsForPlayerOne()
	{
		var actions = new PlayerActions();

		actions.Up.AddDefaultBinding( Key.W );
		actions.Down.AddDefaultBinding( Key.S );
		actions.Right.AddDefaultBinding( Key.D );
		actions.Left.AddDefaultBinding( Key.A );

		actions.Smash.AddDefaultBinding( Key.Space);
		actions.Dash.AddDefaultBinding( Key.LeftShift );

		return actions;
	}

	public static PlayerActions CreateWithKeyboardBindingsForPlayerTwo()
	{
		var actions = new PlayerActions();

		actions.Up.AddDefaultBinding( Key.UpArrow );
		actions.Down.AddDefaultBinding( Key.DownArrow );
		actions.Right.AddDefaultBinding( Key.RightArrow );
		actions.Left.AddDefaultBinding( Key.LeftArrow );

		actions.Smash.AddDefaultBinding( Key.Return );
		actions.Dash.AddDefaultBinding( Key.RightShift );

		return actions;
	}

	public static PlayerActions CreateWithJoystickBindings()
	{
		var actions = new PlayerActions();

		actions.Smash.AddDefaultBinding( InputControlType.Action1 );
		actions.Dash.AddDefaultBinding( InputControlType.Action2 );

		actions.Up.AddDefaultBinding( InputControlType.LeftStickUp );
		actions.Down.AddDefaultBinding( InputControlType.LeftStickDown );
		actions.Left.AddDefaultBinding( InputControlType.LeftStickLeft );
		actions.Right.AddDefaultBinding( InputControlType.LeftStickRight );

		actions.Up.AddDefaultBinding( InputControlType.DPadUp );
		actions.Down.AddDefaultBinding( InputControlType.DPadDown );
		actions.Left.AddDefaultBinding( InputControlType.DPadLeft );
		actions.Right.AddDefaultBinding( InputControlType.DPadRight );

		return actions;
	}
}


