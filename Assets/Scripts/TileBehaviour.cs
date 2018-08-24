using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileBehaviour : MonoBehaviour {
	public bool cracked;
	Animator animator;
	public float x, y;

	void Start () {
		animator = GetComponent<Animator> ();	
		animator.enabled = false;
		x = transform.position.x;
		y = transform.position.y;
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
		return (tile.x >= x - 0.1f && tile.x <= x + 0.1f
				&& tile.y >= y - 0.1f && tile.y <= y + 0.1f);
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
