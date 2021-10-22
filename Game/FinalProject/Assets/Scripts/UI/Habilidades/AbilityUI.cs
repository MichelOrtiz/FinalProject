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
    [SerializeField] GameObject UI;
    [SerializeField] Transform abilitiesHolder;
    [SerializeField] GameObject prefabAbilitySlot;
    [SerializeField] GameObject popUp;
    private void Start() {
        foreach(Ability ab in AbilityManager.instance.abilities){
            GameObject obj = Instantiate(prefabAbilitySlot);
            obj.transform.SetParent(abilitiesHolder,false);
            AbilitySlot slot = obj.GetComponent<AbilitySlot>();
            slot.UpdateUISlot(ab);
            slot.popUp = popUp.GetComponent<PopUpHabUI>();
        }
    }
    public void UpdateUI(){
        foreach(AbilitySlot slot in abilitiesHolder.GetComponentsInChildren<AbilitySlot>()){
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
