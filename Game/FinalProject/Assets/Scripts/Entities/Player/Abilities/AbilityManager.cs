using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public static AbilityManager instance;
    public GameObject abiltySystem;
    private void Awake() {
        if(instance!=null){
            //Badthings
            return;
        }
        instance=this;
    }
    List<Ability> abilities = new List<Ability>();
    public void AddAbility(Ability newAbility){
        abilities.Add(newAbility);
        //añadir componenste
    }
    public void SetActive(bool active){
        foreach(Ability a in abilities){
            a.isUnlocked=active;
        }
    }
}
