using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DudeSounds : MonoBehaviour {

	private AudioSource Audio;

	public AudioClip[] snowClips;
	public AudioClip[] woodClips;
	public AudioClip sneezeClip;
    public AudioClip globePickupClip;

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
	}

	public void PlayFootstep()
    {
		if (isIndoors) {
			Audio.clip = woodClips [Random.Range (0, woodClips.Length - 1)];
		} else {
			Audio.clip = snowClips [Random.Range (0, snowClips.Length - 1)];
		}
		Audio.Play();
	}

	public void PlaySneeze()
    {
		Audio.clip = sneezeClip;
		Audio.Play();
	}

	public void PlayGlobePickup()
    {
		Audio.clip = globePickupClip;
		Audio.Play();
	}
}
