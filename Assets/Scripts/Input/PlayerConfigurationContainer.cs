using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlayerConfigurationContainer {

	public int MAX_PLAYERS = 4;
	public List<PlayerInputConfiguration> Configurations = new List<PlayerInputConfiguration> ();
	private static PlayerConfigurationContainer _instance;

	public static PlayerConfigurationContainer getInstance() {
		if(_instance == null)
			_instance = new PlayerConfigurationContainer();
		return _instance;
	}

	public PlayerInputConfiguration GetConfig(int index) {
		foreach (var config in Configurations) 
			if (config.PlayerColorIndex == index)
				return config;
		return null;
	}

	public void Remove (int index)
	{
		var config = GetConfig (index);
		
		if (config == null)
			return;
		
		Configurations.Remove (config);
	}

	public int GetKeyboardCount() {
		int counter = 0;

		foreach (var config in Configurations)
			if (config.Actions.Device == null)
				counter++;
		return counter;
	}

	public int GetAvailablePlayerIndex() {
		for (int i = 0; i < MAX_PLAYERS; ++i)
			if (GetConfig (i) == null)
				return i;
		return -1;
	}

	public int GetNextAvailablePlayerIndex(int currentIndex) {

		int iter = currentIndex +1;
		while(currentIndex != iter){
			if (iter == MAX_PLAYERS)
				iter = 0;
			
			if (GetConfig (iter) == null)
				return iter;

			iter++;
		}
		return -1;
	}
}
