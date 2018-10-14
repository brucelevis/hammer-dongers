using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.SceneManagement;
using EazyTools.SoundManager;
using UnityEngine.UI;

public class MatchManager : MonoBehaviour {

	public AudioClip soundtrack;
	public static int soundtrackAudioId = -1;
	public GameObject[] grids;
	public GameObject[] players;
	public GameObject victoryMessage;
	public static int maxScore = 3;
	bool gameover = false;
	float matchTransitionTime = 0.75f;
	ScoreCanvasBehaviour scoreCanvas;

	//PlayerIndex => PlayerScore
	public static Dictionary<int, int> scores = new Dictionary<int, int>();

	void Start () {

		scoreCanvas = GameObject.Find ("ScoreCanvas").GetComponent<ScoreCanvasBehaviour> ();

		if(soundtrackAudioId == -1)
			soundtrackAudioId = SoundManager.PlayMusic(soundtrack, 0.1f, true, true, 1, 1);
		
		players = GameObject.FindGameObjectsWithTag("PlayableCharacter");


		foreach(GameObject player in players){
			var index = player.GetComponent<PlayerInput> ().PlayerSuffix;
			if(!scores.ContainsKey(index)){
				scores [index] = 0;
			}
		}
	}

	void UpdateScoreInUI(int playerSuffix){
		scoreCanvas.ShowNextTrophy (playerSuffix);
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

	void SetVictoryMessage (int playerSuffix)
	{
		victoryMessage.SetActive (true);
		Animator animator = victoryMessage.GetComponent<Animator> ();
		animator.Rebind ();
		animator.SetInteger ("Player", playerSuffix);
		gameover = true;
		Invoke ("Restart", 5);
	}

	void Update () {
		
		if (gameover)
			return;
		
		List<GameObject> standingPlayers = players.Where( x => x.activeSelf).ToList();

		if (standingPlayers.Count == 0) 
			Invoke ("Reset", matchTransitionTime);
		else if (standingPlayers.Count == 1){
			int playerSuffix = standingPlayers[0].GetComponent<PlayerInput>().PlayerSuffix;
			scores [playerSuffix]++;
			UpdateScoreInUI(playerSuffix);

			if (scores [playerSuffix] == maxScore) {
				SetVictoryMessage (playerSuffix);
				return;
			}

			if (!gameover) {
				Invoke ("Reset", matchTransitionTime);
				gameover = true;
			}
		}
	}
}
