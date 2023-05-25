using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class Powerbar : MonoBehaviour
{
    public Gradient gradient;
    public Image fill;
    public Slider slider;
    
    public void SetPower(float power) {
        slider.value = power;
        fill.color = gradient.Evaluate(slider.normalizedValue);
    }
    public void SetMaxPower(float power) {
        slider.minValue = power;
        slider.value = power;
        fill.color = gradient.Evaluate(1f);

    }
}
