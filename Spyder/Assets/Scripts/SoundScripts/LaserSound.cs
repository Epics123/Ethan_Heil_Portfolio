using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserSound : MonoBehaviour
{
	private AudioSource mLaser;
	public AudioClip on;
	[Range(0,1)]
	public float onVolume;
	public AudioClip off;
	[Range(0, 1)]
	public float offVolume;
	

	private bool humming;

	private void Start()
	{
		mLaser = GetComponent<AudioSource>();
		mLaser.clip = on;
		mLaser.loop = false;
	}


	

	public void switchOn()
	{
		mLaser.PlayOneShot(on, onVolume);
	}

	public void switchOff()
	{
		mLaser.PlayOneShot(off, offVolume);
	}
}
