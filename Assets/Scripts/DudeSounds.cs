using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSounds : MonoBehaviour {

	private AudioSource Audio;

	public AudioClip[] snowClips;
	public AudioClip[] woodClips;
	public AudioClip sneezeClip;

	private bool isIndoors = false;

	private void Awake()
	{
		EventManager.Listen("ToggleIndoors", ToggleIndoorOutDoor);
	}

	private void OnDisable()
	{
		EventManager.StopListen("ToggleIndoors", ToggleIndoorOutDoor);
	}

	private void ToggleIndoorOutDoor() {
		this.isIndoors = !this.isIndoors;
	}

	private void Start() {
		Audio = GetComponent<AudioSource> ();

		//snowClips = new AudioClip[6];
		//woodClips = new AudioClip[6];
		//
		//for (int i = 1; i <= 6; i++) {
		//	AudioClip snowClip = Resources.Load<AudioClip> ("Sounds/Snowsteps_" + i + "");
		//	AudioClip woodClip = Resources.Load<AudioClip> ("Sounds/Woodstep_" + i + "");
		//	snowClips [i-1] = snowClip;
		//	woodClips [i-1] = woodClip;
		//}
		//sneezeClip = Resources.Load<AudioClip> ("Sounds/Sneeze_1");
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public void PlayFootstep() {
		if (isIndoors) {
			Audio.clip = woodClips [Random.Range (0, woodClips.Length - 1)];
		} else {
			Audio.clip = snowClips [Random.Range (0, snowClips.Length - 1)];
		}
		Audio.Play();

	}

	public void PlaySneeze() {
		Audio.clip = sneezeClip;
		Audio.Play();
	}
}
