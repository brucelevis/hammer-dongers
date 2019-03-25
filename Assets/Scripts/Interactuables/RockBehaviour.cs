using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RockBehaviour : InteractableBehaviour
{
	void Start() {
		_animator = GetComponent<Animator> ();
	}

	public override void OnHit ()
	{
		Debug.Log ("ROCK HIT");
		_animator.SetTrigger ("Break");
	}

	public override void OnTileBreak ()
	{
		_animator.SetTrigger ("Break");
	}
}
