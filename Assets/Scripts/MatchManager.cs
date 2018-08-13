﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;

public class MatchManager : MonoBehaviour {

	public AudioClip soundtrack;
	public static int soundtrackAudioId = -1;
	public GameObject[] grids;
	public GameObject[] players;
	public GameObject victoryMessage;
	bool gameover = false;
	public static Dictionary<int, int> scores = new Dictionary<int, int>();

	// Use this for initialization
	void Awake () {

		if(soundtrackAudioId == -1)
			soundtrackAudioId = SoundManager.PlayMusic(soundtrack, 0.1f, true, true, 1, 1);
		
		players = GameObject.FindGameObjectsWithTag("PlayableCharacter");

		foreach(GameObject player in players){
			var index = player.GetComponent<PlayerInput> ().playerPrefix;
			if(!scores.ContainsKey(index))
				scores [index] = 0;
		}
	}

	void Reset ()
	{
		int index = Random.Range(1, 4);
		SceneManager.LoadScene ("Scene " + index, LoadSceneMode.Single);
	}

	void Restart(){
		scores.Clear ();
		Reset ();
	}

	void SetVictoryMessage (int playerPrefix)
	{
		victoryMessage.SetActive (true);
		Animator animator = victoryMessage.GetComponent<Animator> ();
		animator.Rebind ();
		animator.SetInteger ("Player", playerPrefix);
		gameover = true;
		Invoke ("Restart", 5);
	}

	void Update () {
		if (gameover)
			return;
		
		List<GameObject> standingPlayers = players.Where( x => x.activeSelf).ToList();

		if (standingPlayers.Count == 0) 
			Reset ();
		else if (standingPlayers.Count == 1){
			int playerPrefix = standingPlayers[0].GetComponent<PlayerInput>().playerPrefix;
			scores [playerPrefix]++;

			if (scores [playerPrefix] == 5) {
				SetVictoryMessage (playerPrefix);
				return;
			}
			if(!gameover)
				Reset ();
		}
	}
}
