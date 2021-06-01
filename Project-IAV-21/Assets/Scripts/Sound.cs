using UnityEngine.Audio;
using UnityEngine;

/// <summary>
/// Class for sound instances managment
/// </summary>
[System.Serializable]
public class Sound
{
	public string name;

	public AudioClip clip;

	[Range(0f, 1f)]
	public float volume;
	[Range(.1f, 3f)]
	public float pitch;

	public bool loop;

	[HideInInspector]
	public AudioSource source;
}
