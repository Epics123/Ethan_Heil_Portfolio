using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkingSound : MonoBehaviour
{

	private AudioSource mPlayer;
	public AudioClip step;
	[Range(0, 1)]
	public float stepVolume;
	public float stepFrequency;

	private float mTimer;

	private void Start()
	{
		mTimer = 0.0f;

		mPlayer = GetComponent<AudioSource>();
		mPlayer.loop = false;
	}

	private void Update()
	{
		mTimer += Time.deltaTime;
	}

	//public void playMoveUp()
	//{
	//	if (mTimer >= ratchetClickGap)
	//	{
	//		mPlayer.PlayOneShot(up, upVolume);
	//		mTimer = 0.0f;
	//	}
	//}

	//public void playMoveDown()
	//{
	//	if (mTimer >= ratchetClickGap)
	//	{
	//		mPlayer.PlayOneShot(down, downVolume);
	//		mTimer = 0.0f;
	//	}
	//}
}
