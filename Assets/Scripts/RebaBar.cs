using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RebaBar : MonoBehaviour
{
    public Slider slider;
    public Gradient gradientAscending;
    public Gradient gradientDescending;
    public Image fill;
    public Image border;

    // Step 1: Add bool variable to determine which gradient to use
    [HideInInspector] public bool useAscendingGradient = true;

    public void SetMaxReba(int reba)
    {
        slider.minValue = 1;
        slider.maxValue = reba;
        slider.value = 1;

        // Step 2: Modify this to evaluate the color based on the chosen gradient
        fill.color = useAscendingGradient ? gradientAscending.Evaluate(0f) : gradientDescending.Evaluate(0f);
    }

    public void SetRebaBar(int reba)
    {
        slider.value = reba;

        // Step 2: Modify this to evaluate the color based on the chosen gradient
        float evalValue = 1f - slider.normalizedValue;
        fill.color = useAscendingGradient ? gradientAscending.Evaluate(evalValue) : gradientDescending.Evaluate(evalValue);
    }
}
