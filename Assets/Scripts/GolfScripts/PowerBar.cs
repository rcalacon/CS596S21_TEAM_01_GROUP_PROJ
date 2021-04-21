using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PowerBar : MonoBehaviour
{
    public Slider slider;

    private void Start()
    {
        slider.maxValue = 100;
        slider.value = 0;
    }

    public void SetPower(float power)
    {
        slider.value = power;
    }
}
