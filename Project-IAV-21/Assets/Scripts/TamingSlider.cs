using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

public class TamingSlider : MonoBehaviour
{
    private Slider slider;

    public GameObject gO;
    // Start is called before the first frame update
    void Start()
    {
        slider = GetComponent<Slider>();
        slider.maxValue = (int)Variables.Object(gO).Get("TamingValue");
        slider.value = 0;
    }

    public void UpdateUISlider()
    {
        slider.value = slider.maxValue - (int)Variables.Object(gO).Get("TamingValue");
    }
}
