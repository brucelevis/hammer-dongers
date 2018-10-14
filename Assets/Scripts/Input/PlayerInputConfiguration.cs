public class PlayerInputConfiguration {
	public int PlayerColorIndex;
	public int PlayerSuffix;
	public PlayerActions Actions;

	public PlayerInputConfiguration(int index, PlayerActions actions, int suffix) {
		PlayerColorIndex = index;
		Actions = actions;
		PlayerSuffix = suffix;
	}
}
