using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradient;
    public Image fill;
    public Image border;
    public void SetMaxReba(int reba)
    {
        slider.minValue = 1;
        slider.maxValue = reba;
        slider.value = 1;
        fill.color = gradient.Evaluate(0f);
    }
    public void SetRebaBar(int reba)
    {
        slider.value = reba;
        fill.color = gradient.Evaluate(1f - slider.normalizedValue);
    }
}
