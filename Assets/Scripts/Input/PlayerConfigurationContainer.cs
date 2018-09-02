using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerConfigurationContainer {

	public List<PlayerInputConfiguration> Configurations = new List<PlayerInputConfiguration> ();
	private static PlayerConfigurationContainer _instance;

	public static PlayerConfigurationContainer getInstance() {
		if(_instance == null)
			_instance = new PlayerConfigurationContainer();
		return _instance;
	}
}
