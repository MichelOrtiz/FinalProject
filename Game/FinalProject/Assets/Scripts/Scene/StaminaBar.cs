using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StaminaBar : MonoBehaviour
{
    PlayerManager player = PlayerManager.instance;
    private Slider slider;

    private void Start() {
        slider = gameObject.GetComponent<Slider>();
        player = PlayerManager.instance;
        SetMaxStamina(player.maxStamina);
    }
    private void Update() {
        slider.value = player.currentStamina;
    }

    public void SetMaxStamina(float stamina){
        slider.maxValue = stamina;
        slider.value = stamina;
    }

    public void SetStamina(float stamina){
        slider.value = stamina;
    }
}
