using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSingleton : MonoBehaviour
{
	private static AudioSingleton instance = null;
	public static AudioSingleton Instance
	{
		get { return instance; }
	}

	public AudioClip start;
	public AudioClip loop;

	private AudioSource source;

	private void Awake()
	{
		if(instance != null && instance != this)
		{
			Destroy(this.gameObject);
			return;
		}
		else
		{
			instance = this;
		}
		DontDestroyOnLoad(this.gameObject);
	}

	private void Start()
	{
		source = GetComponent<AudioSource>();
		if (!source.isPlaying)
		{
			source.clip = start;
			source.loop = false;
			source.Play();
		}
	}
	private void Update()
	{
		if(!source.isPlaying && source.clip == start)
		{
			source.clip = loop;
			source.Play();
			source.loop = true;
		}
	}
}
