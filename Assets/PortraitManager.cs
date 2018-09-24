using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PortraitManager : MonoBehaviour {
	
	public bool available = true;
	CharacterSelectActions actions;
	public SpriteRenderer portraitRenderer;
	public int CharacterIndex;
	Animator anim;
	PlayerConfigurationContainer container;

	GameObject leftRightIndicator;

	// Use this for initialization
	void Start () {
		container = PlayerConfigurationContainer.getInstance ();
		anim = GetComponent<Animator>();
		leftRightIndicator = this.transform.Find("ChangeIndicator").gameObject;
	}

	void Update() {

		if(anim.GetCurrentAnimatorStateInfo(0).IsName("Joined") && !leftRightIndicator.activeSelf)
			leftRightIndicator.SetActive(true);
		else if(!anim.GetCurrentAnimatorStateInfo(0).IsName("Joined") && leftRightIndicator.activeSelf)
			leftRightIndicator.SetActive(false);

		if (actions == null)
			return;
		
		if (actions.Leave.WasPressed) {
			Leave ();
			return;
		}

		if (actions.ChangeLeft.WasPressed || actions.ChangeRight.WasPressed)
			SwitchPortrait ();
	}

	public void Join(Sprite portrait, CharacterSelectActions actions, int characterIndex){
		portraitRenderer.sprite = portrait;
		anim.SetTrigger("Join");
		available = false;
		this.actions = actions;
		CharacterIndex = characterIndex;
	}

	public void Leave(){
		anim.SetTrigger("Leave");
		portraitRenderer.sprite = null;
		available = true;
		actions = null;

		container.Remove (CharacterIndex);
	}

	public void SwitchPortrait(){
		var index = container.GetNextAvailablePlayerIndex (CharacterIndex);

		if (index == -1)
			return;

		var config = container.GetConfig (CharacterIndex);
		container.Remove (CharacterIndex);
		config.PlayerIndex = index;
		container.Configurations.Add (config);
		CharacterIndex = index;
		var parentPortraits = GetComponentInParent<PlayerJoin> ().portraitImages;
		portraitRenderer.sprite = parentPortraits[index];
		
	}
}
