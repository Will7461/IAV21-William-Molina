﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

public class LifeSlider : MonoBehaviour
{
    private Slider slider;

    public GameObject gO;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int)Variables.Object(gO).Get("Life");
        slider.value = slider.maxValue;
    }

    public void UpdateUISlider()
    {
        slider.value = (int)Variables.Object(gO).Get("Life");
    }
}