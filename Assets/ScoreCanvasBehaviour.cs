using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreCanvasBehaviour : MonoBehaviour {
	
	public List<GameObject> PlayerScorePrefabs;
	Dictionary<int, ScoreBarBehaviour> Scores;

	static int GetScore (ScoreBarBehaviour scoreBar)
	{
		return MatchManager.scores.ContainsKey (scoreBar.PlayerSuffix) ? MatchManager.scores [scoreBar.PlayerSuffix] : 0;
	}

	void Start () {
		var container = PlayerConfigurationContainer.getInstance ();
		Scores = new Dictionary<int, ScoreBarBehaviour> ();
		var count = container.Configurations.Count;

		foreach (var config in container.Configurations) {
			var instance = Instantiate (PlayerScorePrefabs[count - 2], this.transform.position, this.transform.rotation, this.transform);
			instance.transform.SetParent (this.transform);

			var image = instance.GetComponent<Image>();
			var imageWidth = image.sprite.rect.width;
			instance.transform.position = new Vector3 ((Screen.width/count * (config.PlayerSuffix - 1)), 0, 1);

			var scoreBar = instance.GetComponent<ScoreBarBehaviour> ();
			scoreBar.PlayerColorIndex = config.PlayerColorIndex;
			scoreBar.PlayerSuffix= config.PlayerSuffix;
			scoreBar.PlayerCount = count; 
			scoreBar.Score = GetScore (scoreBar);
			Scores.Add (scoreBar.PlayerSuffix, scoreBar);
		}
	}

	public void ShowNextTrophy (int playerSuffix)
	{
		Scores[playerSuffix].ShowNextTrophy ();
	}
	
}
