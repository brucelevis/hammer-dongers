using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectTitleBehaviour : MonoBehaviour {

	GameObject banners;
	Animator anim;
	// Use this for initialization
	void Start () {
		banners = GameObject.Find("CharacterSlots");
		anim = GetComponent<Animator>();
	}
	
	// Update is called once per frame
	void LateUpdate () {
		var portraits = banners.GetComponentsInChildren(typeof(PortraitManager), true);
		
		int takenSpots = 0;

		foreach(PortraitManager portrait in portraits){
			if(!portrait.available)
				takenSpots++;
		}

		Debug.Log(takenSpots);

		if(takenSpots >= 2)
			anim.SetBool("Ready", true);
		else
			anim.SetBool("Ready", false);
	}
}
