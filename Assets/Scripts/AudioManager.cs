using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using EazyTools.SoundManager;

public class AudioManager : MonoBehaviour {

	public static AudioManager _instance;

	public static Dictionary<string, AudioClip> sfxByName = new Dictionary<string, AudioClip>();
	public static AudioSource audioSource;
	public AudioClip[] allClips;
	
	void Start () {
		audioSource = GetComponent<AudioSource>();
		foreach(AudioClip clip in allClips){
			sfxByName[clip.name] = clip;
		}
	}
	
	public static void playSFX (string name, float volume, bool cutMultiple){

		if(cutMultiple){
			var sfx = sfxByName[name];
			audioSource.clip = sfx;
			audioSource.volume = volume;
			audioSource.Play();
		}else
			audioSource.PlayOneShot(sfxByName[name], volume);
	}
}
