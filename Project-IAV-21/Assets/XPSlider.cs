using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Bolt;

public class XPSlider : MonoBehaviour
{
    private Slider slider;

    public GameObject gO;
    // Start is called before the first frame update
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