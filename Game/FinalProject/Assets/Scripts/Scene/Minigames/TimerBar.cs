using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerBar : MonoBehaviour
{
    public Slider slider;

    public void SetMaxTime(float time){
        slider.maxValue = time;
        slider.value = time;
    }

    public void SetTime(float time){
        slider.value = time;
    }
}
