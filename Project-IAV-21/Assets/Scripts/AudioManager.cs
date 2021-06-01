using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
	/// <summary>
	/// Sound's array
	/// </summary>
	[Tooltip("Sound's array")]
	public Sound[] sounds;

	public static AudioManager instance;

	private void Awake()
	{
		if (instance == null)
			instance = this;
		else
		{
			Destroy(gameObject);
			return;
		}

		DontDestroyOnLoad(gameObject);
		// Set every Sound attribute
	    foreach(Sound s in sounds)
		{
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
			s.source.loop = s.loop;
		}
	}
	/// <summary>
	/// Music start
	/// </summary>
	void Start()
    {
        Play("Theme");
    }

	/// <summary>
	/// Play a sound by name
	/// </summary>
	/// <param name="name">name of a sound</param>
	public void Play(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("No sound name " + name);
			return;
		}
		s.source.Play();
	}
	/// <summary>
	/// Get an AudioSource by name
	/// </summary>
	/// <param name="name">name of a sound</param>
	/// <returns>AudioSource of a name sound</returns>
	public AudioSource GetAudioSource(string name)
	{
		Sound s = Array.Find(sounds, sound => sound.name == name);
		if (s == null)
		{
			Debug.LogWarning("No sound name " + name);
			return null;
		}
		return s.source;
	}
}
