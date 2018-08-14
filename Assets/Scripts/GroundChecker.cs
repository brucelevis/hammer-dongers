using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundChecker : MonoBehaviour {
	public void Reset ()
	{
		_count = 0;
	}

	public int _count;
	PlayerBehaviour player;

	bool isEmpty ()
	{
		return _count == 0;
	}

	void Start() {
		player = GetComponentInParent<PlayerBehaviour> ();
	}

	void LateUpdate() {
		if (isEmpty () && Time.timeSinceLevelLoad > 0.04f) 
		{
			player.Die ();
		}
	}

	void OnTriggerEnter2D(Collider2D collider) {
		++_count;
	}

	void OnTriggerExit2D(Collider2D collider) {
		--_count;
	}
}
