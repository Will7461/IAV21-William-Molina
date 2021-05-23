using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Bolt;

public class Health : MonoBehaviour
{
    public LifeSlider lifeSlider;

    // Update is called once per frame
    public void takeDamage(int dmg)
    {
        int life = (int)Variables.Object(gameObject).Get("Life");
        life -= dmg;
        Variables.Object(gameObject).Set("Life", life);
        lifeSlider.UpdateUISlider();
    }
}
