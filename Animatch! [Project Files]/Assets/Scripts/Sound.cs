using System;
using UnityEngine;
using UnityEngine.Audio;

[System.Serializable] // allow objects of this type to appear in the inspector

public class Sound // to create sound gameobjects with specific properties
{
	public string name;
	public int type; // music or effect

	public AudioClip clip;

	[Range(0f, 1f)] // modify through slider
	public float volume;

	public bool loop; // looping is set to true for background music

	[HideInInspector]
	public AudioSource source; // will be set from manager
}