using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unit_sounds : MonoBehaviour {

	public AudioClip[] clipsAcknowledge;
	public AudioClip[] clipsSelected;

	private Dictionary<string, AudioSource[]> sounds;

	public AudioSource AddAudio(AudioClip clip, bool loop, bool playAwake, float vol) { 
		AudioSource newAudio = gameObject.AddComponent<AudioSource>();
		newAudio.clip = clip;
		newAudio.loop = loop;
		newAudio.playOnAwake = playAwake;
		newAudio.volume = vol; 
		return newAudio; 
	}

	public void AddClips(string name, ref AudioClip[] clips) {
		AudioSource[] sources = new AudioSource[clips.Length];
		for (int i = 0; i < clips.Length; ++i) {
			sources [i] = AddAudio (clips [i], false, false, 1.0f);
		}
		try {
			sounds[name] = sources;
		} catch(UnityException) {
			Debug.Log ("Key already in sounds: "+name);
		}
	}

	// Use this for initialization
	void Start () {
		sounds = new Dictionary<string, AudioSource[]> ();
		AddClips ("Acknowledge", ref clipsAcknowledge);
		AddClips ("Selected", ref clipsSelected);
	}

	public void Play(string name) {
		try {
			AudioSource[] array = sounds[name];
			if (array != null && array.Length > 0) {
				array[Random.Range(0, array.Length)].Play();
			}
		} catch(KeyNotFoundException) {
			Debug.Log ("Key not found in sounds: "+name);
		}
	}

	// Update is called once per frame
	void Update () {
	
	}
}
