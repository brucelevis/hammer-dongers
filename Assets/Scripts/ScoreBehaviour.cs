using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

public class ScoreBehaviour : MonoBehaviour {

	GameObject scoreBar;
	public GameObject playerScorePrefab;

	void InstantiatePlayerScores ()
	{
		var scores = MatchManager.scores;
		var keys = scores.Keys;

		var playerScores = keys.Select ((key, i) => {
			var gameObject = Instantiate (playerScorePrefab, transform.position, transform.rotation, scoreBar.transform);
			var image = gameObject.GetComponentInChildren<Image>();

			Camera cam = Camera.main;
			float height = 2f * cam.orthographicSize;
			float width = height * cam.aspect;

			var x = 8 + image.sprite.rect.width + (Screen.width/6 * i);
			var y = image.sprite.rect.height;
			var z = transform.position.z;

			var rect = gameObject.GetComponent<RectTransform>();
			rect.localPosition = new Vector3(x,y,z);
			return gameObject;
		}
		).ToList();

		Debug.Log ("");
	}

	// Use this for initialization
	void Start () {
		scoreBar = GameObject.Find ("ScoreBar");
		InstantiatePlayerScores ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
