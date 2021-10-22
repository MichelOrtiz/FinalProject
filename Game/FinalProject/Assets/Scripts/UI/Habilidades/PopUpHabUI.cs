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
    [SerializeField] GameObject isPasiveNotice;
    [SerializeField] TMP_Dropdown hotkey;
    public void UpdateUI(Ability ability){
        this.ability = ability;
        icon.sprite = ability.iconAbility;
        name_Ab.text = ability.abilityName.ToString();
        desc_Ab.text = ability.description;
        if(!ability.isPasive){
            groupHotkey.SetActive(true);
            isPasiveNotice.SetActive(false);
            hotkey.ClearOptions();
            TMP_Dropdown.OptionData currentHotKey = null;
            List<TMP_Dropdown.OptionData> options = new List<TMP_Dropdown.OptionData>();
            foreach(KeyCode key in PlayerManager.instance.inputs.skillHotkeys){
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = key.ToString();
                options.Add(option);
                if(key.ToString().Equals(ability.hotkey.ToString())){
                    currentHotKey = option;
                }
            }
            TMP_Dropdown.OptionData opt = new TMP_Dropdown.OptionData();
            opt.text = "Ninguna";
            options.Add(opt);
            hotkey.AddOptions(options);
            if(currentHotKey == null) currentHotKey = opt;
            hotkey.value = hotkey.options.IndexOf(currentHotKey);
            
        }
        else if(ability.isPasive){
            groupHotkey.SetActive(false);
            isPasiveNotice.SetActive(true);
        }
    }
    public void GoBack(){
        holder.SetActive(true);
        gameObject.SetActive(false);
        string key = hotkey.options[hotkey.value].text;
        if(key == "Ninguna"){
            ability.hotkey = KeyCode.None;
        }else{
            ability.hotkey = (KeyCode) System.Enum.Parse(typeof(KeyCode),key);
        }
        
    }

}
