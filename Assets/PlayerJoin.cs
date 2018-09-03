using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour {

	public Sprite[] portraitImages;
	PortraitManager[] portraitBanners;
	CharacterSelectActions actions;

	void Start () {
		portraitBanners = GetComponentsInChildren<PortraitManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	PortraitManager GetAvailableBanner(){
		foreach(PortraitManager portrait in portraitBanners){
			if(portrait.available)
				return portrait;
		}
		return null;
	}

	public void Join(int colorIndex, CharacterSelectActions bannerActions){
		var banner = GetAvailableBanner ();
		banner.Join (portraitImages[colorIndex], bannerActions, colorIndex);
	}
}


