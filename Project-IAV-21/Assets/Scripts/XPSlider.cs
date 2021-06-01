using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;
/// <summary>
/// Class for slider managment based on xp value
/// </summary>
public class XPSlider : MonoBehaviour
{
    private Slider slider;

    public GameObject gO;

    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int)Variables.Object(gO).Get("MaxXP");
        slider.value = 0;
    }

    public void UpdateUISlider()
    {
        if (slider != null)
        slider.value = (int)Variables.Object(gO).Get("XP");
    }
}