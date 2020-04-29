using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMoveSound : MonoBehaviour
{
	public float landSoundGap;

	private AudioSource mPlayer;
	public AudioClip land;
	[Range(0, 1)]
	public float landVolume;

	private float mTimer;

	private void Start()
	{
		mTimer = 0.0f;

		mPlayer = GetComponent<AudioSource>();
		mPlayer.clip = land;
		mPlayer.loop = false;
	}

	private void Update()
	{
		mTimer += Time.deltaTime;
	}

	//Just launch - for when target is out of range
	public void playLand()
	{
		if (mTimer >= landSoundGap)
		{
			mPlayer.PlayOneShot(land, landVolume);
			mTimer = 0.0f;
		}
	}
}
