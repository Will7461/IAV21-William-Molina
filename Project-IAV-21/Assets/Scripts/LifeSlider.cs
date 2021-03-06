using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;
/// <summary>
/// Class for slider managment based on Life values
/// </summary>
public class LifeSlider : MonoBehaviour
{
    private Slider slider;

    public GameObject gO;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int)Variables.Object(gO).Get("MaxLife");
        slider.value = slider.maxValue;
    }

    public void UpdateUISlider()
    {
        if(slider!=null)
        slider.value = (int)Variables.Object(gO).Get("Life");
    }
}
