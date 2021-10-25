using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityUI : MonoBehaviour
{
    public static AbilityUI instance;
    private void Awake() {
        if(instance == null){
            instance = this;
        }else{
            Destroy(this);
        }
    }
    public GameObject UI;
    [SerializeField] Transform abilitiesHolder;
    //[SerializeField] GameObject prefabAbilitySlot;
    public AbilitySlot[] slots { get => abilitiesHolder.GetComponentsInChildren<AbilitySlot>();}
    [SerializeField] GameObject popUp;
    private void Start() {
        int i = 0;
        foreach(Ability ab in AbilityManager.instance.abilities){
            slots[i].UpdateUISlot(ab);
            slots[i].popUp = popUp.GetComponent<PopUpHabUI>();
            i++;
        }
    }
    public void UpdateUI(){
        foreach(AbilitySlot slot in slots){
            slot.UpdateUISlot();
        }
    }
    public void SetOpen(bool isOpen){
        UI.SetActive(isOpen);
    }
    public bool GetOpen(){
        return UI.activeSelf;
    }
}
