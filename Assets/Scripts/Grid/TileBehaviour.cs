﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
	public bool cracked;
	Animator animator;
	public float x, y;
	public float timer = 3;
	public int CollisionCounter = 0;
	GridBehaviour grid;

	void Start () {
		grid = GetComponentInParent<GridBehaviour> ();
		animator = GetComponent<Animator> ();	
		x = transform.position.x;
		y = transform.position.y;
	}

	void PlaySfx ()
	{
		AudioManager.playSFX("break", 0.75f, true);
	}

	public void Crack ()
	{
		if (cracked)
			return;
		cracked = true;
		animator.SetBool ("Cracked", true);
	}

	public void Update () {
		if (cracked)
			return;
		ConsumeTimer ();
	}

	void UpdateTileState ()
	{
		int state = (int)Mathf.Ceil (timer);
		if (animator.GetInteger ("Timer") != state)
			animator.SetInteger ("Timer", state);
	}

	void ConsumeTimer ()
	{
		if (Time.timeSinceLevelLoad < 2)
			return; 
		
		if (CollisionCounter > 0) 
			timer -= Time.deltaTime;
		
		if (timer <= 0) {
			Crack ();
			grid.Crack (this);
		}

		UpdateTileState ();
	}

	public override bool Equals (object other)
	{
		TileBehaviour tile = (TileBehaviour)other;
		return (tile.x >= x - 0.1f && tile.x <= x + 0.1f
				&& tile.y >= y - 0.1f && tile.y <= y + 0.1f);
	}

	public void InvokeCrack (float time)
	{
		Invoke ("Crack", time);
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground Hitbox") 
			CollisionCounter++;
	}

	void OnTriggerExit2D(Collider2D col)
	{
		if (col.gameObject.tag == "Ground Hitbox")
			CollisionCounter--;
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}
}
