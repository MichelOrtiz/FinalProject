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
    [SerializeField] GameObject prefabAbilitySlot;
    [SerializeField] GameObject popUp;
    
    public void UpdateUI(List<Ability> abilities){
        foreach(Ability ab in abilities){
            GameObject obj = Instantiate(prefabAbilitySlot);
            obj.transform.SetParent(abilitiesHolder,false);
            AbilitySlot slot = obj.GetComponent<AbilitySlot>();
            slot.UpdateUISlot(ab);
            slot.popUp = popUp.GetComponent<PopUpHabUI>();
        }
    }
    public void SetOpen(bool isOpen){
        UI.SetActive(isOpen);
    }
    public bool GetOpen(){
        return UI.activeSelf;
    }
}
