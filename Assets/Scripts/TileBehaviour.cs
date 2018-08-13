using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
	public bool cracked;
	Animator animator;
	public int x,y;

	void Start () {
		animator = GetComponent<Animator> ();	
		animator.enabled = false;
		x = (int)Mathf.Floor(transform.position.x);
		y = (int)Mathf.Floor(transform.position.y);
	}

	void PlaySfx ()
	{
		AudioManager.playSFX("break", 0.75f, true);
	}

	public void Crack()
	{
		if (cracked)
			return;
		animator.enabled = true;
		cracked = true;
	}



	public override bool Equals (object other)
	{
		TileBehaviour tile = (TileBehaviour)other;
		return tile.x == x && tile.y == y;
	}

	public void InvokeCrack (float time)
	{
		Invoke ("Crack", time);
	}

	public override int GetHashCode ()
	{
		return base.GetHashCode ();
	}
}
