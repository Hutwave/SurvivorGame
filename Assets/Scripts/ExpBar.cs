using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ExpBar : MonoBehaviour
{

    public Slider slider;

    public void onAwake()
    {
        slider = GetComponent<Slider>();
    }

    public void SetMaxExp(int maxExp)
    {
        slider.minValue = 0;
        slider.maxValue = maxExp;
    }

    public void SetCurrentExp(int exp)
    {
        slider.value = exp;
    }
}
