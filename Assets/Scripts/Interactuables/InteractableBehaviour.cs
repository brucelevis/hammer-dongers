using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class InteractableBehaviour : MonoBehaviour
{
	public abstract void OnHit ();
	public abstract void OnTileBreak ();
	protected Animator _animator;

	public void DestroySelf() {
		Destroy (this.gameObject);
	}
}
