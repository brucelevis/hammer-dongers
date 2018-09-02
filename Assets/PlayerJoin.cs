using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerJoin : MonoBehaviour {

	public Sprite[] portraitImages;
	PortraitManager[] portraitBanners;

	void Start () {
		portraitBanners = GetComponentsInChildren<PortraitManager>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
	

	void Join(){
		
	}
}
