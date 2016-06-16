using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class unit_sounds : MonoBehaviour {

	public AudioClip[] clipsAcknowledge;
	public AudioClip[] clipsSelected;
	public AudioClip[] clipsDead;
	public AudioClip[] clipsAttack;
	public float attack_interval = 2.0f;

	private Dictionary<string, AudioSource[]> sounds;
	private float attack_timer;

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
		attack_timer = 0.0f;
		AddClips ("Acknowledge", ref clipsAcknowledge);
		AddClips ("Selected", ref clipsSelected);
		AddClips ("Dead", ref clipsDead);
		AddClips ("Attack", ref clipsAttack);
	}

	public int Play(string name) {
		int length = 0;
		try {
			AudioSource[] array = sounds[name];
			if (array != null && array.Length > 0) {
//				Debug.Log ("Playing sound \""+name+"\"");
				int i = Random.Range(0, array.Length);
				array[i].Play();
				length = (int)array[i].clip.length;
			}
		} catch(KeyNotFoundException) {
			Debug.Log ("Key not found in sounds: "+name);
		}
		return length;
	}

	public int Attack() {
		if (attack_timer >= attack_interval) {
			attack_timer = 0.0f;
			return Play ("Attack");
		}
		return 0;
	}

	// Update is called once per frame
	void Update () {
		attack_timer += Time.deltaTime;
	}
}
