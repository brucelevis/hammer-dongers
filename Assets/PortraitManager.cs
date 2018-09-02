using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitManager : MonoBehaviour {

	public int defaultPortrait = 0;

	SpriteRenderer portraitRenderer;
	Animator anim;

	// Use this for initialization
	void Start () {
		portraitRenderer = GetComponentInChildren<SpriteRenderer>();
		anim = GetComponent<Animator>();
	}

	public void join(Sprite portrait){
		portraitRenderer.sprite =portrait;
		anim.SetTrigger("Join");
	}

	public void leave(){
		anim.SetTrigger("Leave");
		portraitRenderer.sprite = null;
	}

	public void switchPortrait(Sprite portrait){
		portraitRenderer.sprite =portrait;
	}
}
