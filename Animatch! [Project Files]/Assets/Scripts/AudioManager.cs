using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour // audio management for all scenes
{
	public static AudioManager instance;
	public Sound[] sounds;

	void Awake()
	{
		// allow only one instance of the audiomanager to exist in one scene and avoid duplicates
		if (instance != null)
		{
			Destroy(gameObject);
		}
		else
		{
			instance = this;
			DontDestroyOnLoad(gameObject);
		}

		foreach (Sound s in sounds)
		{
			// add audiosource and parameters for the sound
			s.source = gameObject.AddComponent<AudioSource>();
			s.source.clip = s.clip;
			s.source.loop = s.loop;
		}
	}

	public void Play(string soundName) // to play a sound whose name is passed
	{
		Sound s = Array.Find(sounds, item => item.name == soundName);
		if (s == null)
		{
			Debug.LogWarning("Invalid sound name - play");
			return;
		}
		s.source.volume = s.volume;
		if (s.type == 0)
			s.source.Play();
		else
			s.source.PlayOneShot(s.clip);
	}

	public void Stop(string soundName) // to stop a sound whose name is passed
	{
		Sound s = Array.Find(sounds, item => item.name == soundName);
		if (s == null)
		{
			Debug.LogWarning("Invalid sound name - stop");
			return;
		}
		s.source.Stop();
	}

	public void StopAll() // to stop all sounds
	{
		foreach (Sound s in sounds)
		{
			s.source.Stop();
		}
	}

	public void CrossFade(string stop, string start) // crossfade between stop and start audio clips
	{
		StartCoroutine(Fade(2.0f, stop, start));
	}

	public IEnumerator Fade(float duration, string stop, string start) // crossfade coroutine
	{
		Sound s1 = Array.Find(sounds, item => item.name == stop);
		Sound s2 = Array.Find(sounds, item => item.name == start);

		float actualVol = s1.source.volume;
		float currentVol;
		currentVol = s1.source.volume;

		float currentTime = 0;
		while (currentTime <= duration / 2) // fade out
		{
			currentTime += Time.unscaledDeltaTime; // prevents it from being affected by timescale during game pause state
			float newVol = Mathf.Lerp(currentVol, 0.0f, (currentTime + 1) / duration); // lower volume gradually depending on time passed
			s1.source.volume = newVol;
			if (currentTime >= duration / 2)
			{
				Stop(stop);
				s1.source.volume = actualVol;
			}
			yield return null;
		}

		actualVol = s2.source.volume;
		s2.source.volume = 0.0f;
		Play(start);
		currentTime = 0;
		currentVol = 0.0f;

		while (currentTime <= duration / 2) // fade in
		{
			currentTime += Time.unscaledDeltaTime; // prevents it from being affected by timescale during game pause state
			float newVol = Mathf.Lerp(currentVol, actualVol, (currentTime + 1) / duration); // raise volume gradually depending on time passed
			s2.source.volume = newVol;
			yield return null;
		}
		yield break;
	}

	public void Update()
    {
		GameObject lvl = GameObject.FindGameObjectWithTag("LevelManager");
		var myScript = lvl.GetComponent<LevelManager>();
		foreach (Sound s in sounds)
		{
			s.source.mute = !myScript.sound;
		}
	}
}
