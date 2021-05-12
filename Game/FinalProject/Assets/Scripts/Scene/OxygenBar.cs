using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OxygenBar : MonoBehaviour
{
    PlayerManager player = PlayerManager.instance;
    private Slider slider;
    private void Start() {
        slider = gameObject.GetComponent<Slider>();
        player = PlayerManager.instance;
        SetMaxOxygen(player.maxOxygen);
    }
    private void Update() {
        slider.value = player.currentOxygen;
    }
    public void SetMaxOxygen(float oxygen){
        slider.maxValue = oxygen;
        slider.value = oxygen;
    }

    public void SetOxygen(float oxygen){
        slider.value = oxygen;
    }
}