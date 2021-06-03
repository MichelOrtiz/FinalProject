using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities = new List<Ability>();
    public static AbilityManager instance;
    public GameObject abiltySystem;
    private void Awake() {
        if(instance!=null){
            //Badthings
            return;
        }
        instance=this;
    }
    private void Start() {
        Ability[] abs = abiltySystem.GetComponents<Ability>();
        for(int i=0;i<abs.Length;i++){
            abilities.Add(abs[i]);
        }
    }
    public void SetActive(bool active){
        foreach(Ability a in abilities){
            a.isUnlocked=active;
            a.enabled=active;
        }
    }
}
