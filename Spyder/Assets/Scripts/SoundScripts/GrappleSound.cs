using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrappleSound : MonoBehaviour
{
	public float ratchetClickGap;

	private AudioSource mPlayer;
	public AudioClip miss;
	[Range(0,1)]
	public float missVolume;
	public AudioClip hit;
	[Range(0, 1)]
	public float hitVolume;
	public AudioClip up;
	[Range(0, 1)]
	public float upVolume;
	public AudioClip down;
	[Range(0, 1)]
	public float downVolume;

	private float mTimer;

	private void Start()
	{
		mTimer = 0.0f;

		mPlayer = GetComponent<AudioSource>();
		mPlayer.clip = miss;
		mPlayer.loop = false;
	}

	private void Update()
	{
		mTimer += Time.deltaTime;
	}

	//Just launch - for when target is out of range
	public void playLaunch()
	{
		mPlayer.PlayOneShot(miss, missVolume);
	}

	//Launch & hit - for when the hook attaches
	public void playHit()
	{
		mPlayer.PlayOneShot(hit, hitVolume);
	}

	public void playMoveUp()
	{
		if (mTimer >= ratchetClickGap)
		{
			mPlayer.PlayOneShot(up, upVolume);
			mTimer = 0.0f;
		}
	}

	public void playMoveDown()
	{
		if (mTimer >= ratchetClickGap)
		{
			mPlayer.PlayOneShot(down, downVolume);
			mTimer = 0.0f;
		}
	}
}
