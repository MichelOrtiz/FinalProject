using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityManager : MonoBehaviour
{
    List<Ability> abilities = new List<Ability>();
    public void AddAbility(Ability newAbility){
        abilities.Add(newAbility);
    }
    public void SetActive(bool active){
        foreach(Ability a in abilities){
            a.isUnlocked=active;
        }
    }
}
