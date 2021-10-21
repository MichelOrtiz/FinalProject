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
    [SerializeField] GameObject groupHotkey;
    [SerializeField] TextMeshProUGUI hotkey_text;
    public void UpdateUI(Ability ability){
        this.ability = ability;
        icon.sprite = ability.iconAbility;
        name_Ab.text = ability.abilityName.ToString();
        desc_Ab.text = ability.description;
        if(ability.hotkey != KeyCode.None){
            hotkey_text.text = "<" + ability.hotkey.ToString() + ">";
        }
        else{
            hotkey_text.text = "<Sin asignar>";
        }
    }
    public void GoBack(){
        holder.SetActive(true);
        gameObject.SetActive(false);
    }
}
