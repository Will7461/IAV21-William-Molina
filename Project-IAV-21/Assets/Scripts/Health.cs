﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;
using UnityEngine.Assertions;

public class Health : MonoBehaviour
{
    public GameObject healthBarCanvas;

    private bool isBarActive = false;

    // Update is called once per frame
    public void takeDamage(int dmg)
    {
        int life = (int)Variables.Object(gameObject).Get("Life");
        life -= dmg;
        Variables.Object(gameObject).Set("Life", life);
        if(getHealthBarActive())
        healthBarCanvas.GetComponentInChildren<LifeSlider>().UpdateUISlider();
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
