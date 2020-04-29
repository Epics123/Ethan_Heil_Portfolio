using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSound : MonoBehaviour
{
	public float shutterGap;

	private AudioSource mCamera;
	public AudioClip shutter;
	[Range(0, 1)]
	public float shutterVolume;


	private float mTimer;

	private void Start()
	{
		mTimer = 0.0f;

		mCamera = GetComponent<AudioSource>();
		mCamera.clip = shutter;
		mCamera.loop = false;
	}

	private void Update()
	{
		mTimer += Time.deltaTime;
	}

	public void playShutter()
	{
		if (mTimer >= shutterGap)
		{
			mCamera.PlayOneShot(shutter, shutterVolume);
			mTimer = 0.0f;
		}
	}
}
