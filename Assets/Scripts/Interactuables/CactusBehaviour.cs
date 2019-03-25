﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CactusBehaviour : InteractableBehaviour {
	void Start() {
		_animator = GetComponent<Animator> ();
	}

	void Update() {
		
	}

	public override void OnHit ()
	{
		_animator.SetTrigger ("Break");
	}

	public override void OnTileBreak ()
	{
		_animator.SetTrigger ("Fall");
	}
}
