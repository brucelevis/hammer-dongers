using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScoreBarBehaviour : MonoBehaviour {

	public int PlayerColorIndex;
	public int PlayerSuffix;
	public int Score;
	public int PlayerCount;
	List<GameObject> Points;
	Image image;

	void Start () {
		int children = transform.childCount;

		for (int i = 0; i < MatchManager.maxScore; ++i) {
			var child = transform.GetChild (i);
			child.gameObject.SetActive (true);

			if (Score > i) {
				var animator = child.GetComponent<Animator> ();
				animator.SetTrigger ("trophy");
			}
		}

		SetColor ();
	}

	public void ShowNextTrophy() {
		var child = transform.GetChild (Score);
		var animator = child.GetComponent<Animator> ();
		animator.SetTrigger ("showTrophy");
	}

	void SetColor ()
	{
		var swap = GetComponent<SwapTextureFromImage> ();
		swap.targetColors= CharacterColors.GetPalette (0);
		swap.swapColors = CharacterColors.GetPalette (PlayerColorIndex);
	}
}
