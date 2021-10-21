using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PopUpHabUI : MonoBehaviour
{
    [HideInInspector] public Ability ability;
    [SerializeField] Image icon;
    [SerializeField] TextMeshProUGUI name_Ab;
    [SerializeField] TextMeshProUGUI desc_Ab;
    [SerializeField] GameObject holder;
    public void UpdateUI(Ability ability){
        this.ability = ability;
        icon.sprite = ability.iconAbility;
        name_Ab.text = ability.abilityName.ToString();
        desc_Ab.text = ability.description;
    }
    public void GoBack(){
        holder.SetActive(true);
        gameObject.SetActive(false);
    }
}
