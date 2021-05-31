using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.Assertions;

public class Health : MonoBehaviour
{
    public GameObject healthBarCanvas;
    public GameObject xpBarCanvas;

    public string sound;

    private bool isBarActive = false;

	public void Start()
	{
        AudioSource myAudioSource = gameObject.GetComponent<AudioSource>();
        AudioSource amAudioSource = AudioManager.instance.GetAudioSource(sound);
        myAudioSource.name = amAudioSource.name;
        myAudioSource.clip = amAudioSource.clip;
        myAudioSource.volume = amAudioSource.volume;
        myAudioSource.pitch = amAudioSource.pitch;
        myAudioSource.loop = amAudioSource.loop;
	}
    public void PlaySound()
	{
        GetComponent<AudioSource>().Play();
	}
	public void takeDamage(int dmg)
    {
        int life = (int)Variables.Object(gameObject).Get("Life");
        life -= dmg;
        if (life < 0) life = 0;
        Variables.Object(gameObject).Set("Life", life);
        healthBarCanvas.GetComponentInChildren<LifeSlider>().UpdateUISlider();
    }

    public void healLife(int points)
    {
        int life = (int)Variables.Object(gameObject).Get("Life");
        if(life < (int)Variables.Object(gameObject).Get("MaxLife"))
		{
            life += points;
            if (life > (int)Variables.Object(gameObject).Get("MaxLife")) life = (int)Variables.Object(gameObject).Get("MaxLife");
            Variables.Object(gameObject).Set("Life", life);
            healthBarCanvas.GetComponentInChildren<LifeSlider>().UpdateUISlider();
        }
    }

    public void earnXP()
    {
        if ((int)Variables.Object(gameObject).Get("DangerLevel") == 0) return;
        int xp = (int)Variables.Object(gameObject).Get("XP");
        if (xp >= (int)Variables.Object(gameObject).Get("MaxXP"))
		{
            Variables.Object(gameObject).Set("GoodBoy", true);
            return;
        }
        PlaySound();
        xp++;
        Variables.Object(gameObject).Set("XP", xp);
        xpBarCanvas.GetComponentInChildren<XPSlider>().UpdateUISlider();
    }

    public void showHealthBar()
    {
        isBarActive = true;
        healthBarCanvas.SetActive(true);
    }
    public void hideHealthBar()
    {
        isBarActive = false;
        healthBarCanvas.SetActive(false);
    }

    public bool getHealthBarActive()
	{
        return isBarActive;
	}
}
