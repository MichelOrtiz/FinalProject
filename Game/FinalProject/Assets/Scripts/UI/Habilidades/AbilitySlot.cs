using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class AbilitySlot : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [HideInInspector] public Ability ability;
    [HideInInspector] public PopUpHabUI popUp;
    [SerializeField] Image ab_img;
    [SerializeField] Image cover;
    public void OnPointerEnter(PointerEventData data){
        if(!ability.isUnlocked) return;
        ab_img.color = new Color(0.8f,0.2f,0.2f);
    }
    public void OnPointerExit(PointerEventData data){
        if(!ability.isUnlocked) return;
        ab_img.color = Color.white;
    }
    public void OnClickBtn(){
        ab_img.color = Color.white;
        popUp.UpdateUI(ability);
    }
    public void UpdateUISlot(Ability ability){
        this.ability = ability;
        ab_img.sprite = ability.iconAbility;
        if(ability.isUnlocked){
            cover.gameObject.SetActive(false);
            gameObject.GetComponent<Button>().enabled = true;
        }
        else{
            cover.gameObject.SetActive(true);
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
    public void UpdateUISlot(){
        if(ability == null) return;
        if(ability.isUnlocked){
            cover.gameObject.SetActive(false);
            gameObject.GetComponent<Button>().enabled = true;
        }
        else{
            cover.gameObject.SetActive(true);
            gameObject.GetComponent<Button>().enabled = false;
        }
    }
}
