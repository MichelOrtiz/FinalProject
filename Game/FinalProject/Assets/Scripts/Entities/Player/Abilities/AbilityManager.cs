using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class AbilityManager : MonoBehaviour
{
    public List<Ability> abilities {
        get{
            return abiltySystem.GetComponents<Ability>().ToList<Ability>();
        }
    }
    public static AbilityManager instance;
    public GameObject abiltySystem;
    public AbilityUI abilityUI;
    private void Awake() {
        if(instance!=null){
            //Badthings
            return;
        }
        instance=this;
    }
    private void Start() {
        //Load saved abilities in savefiles
        abilityUI = AbilityUI.instance;
        if(SaveFilesManager.instance != null){
            SaveFile partida = SaveFilesManager.instance.currentSaveSlot;
            if(partida.unlockedAbilities != null){
                int i = 0;
                foreach(Ability a in abilities){
                    if( i > partida.unlockedAbilities.Length) break;
                    a.isUnlocked = partida.unlockedAbilities[i];
                    a.enabled = a.isUnlocked;
                    i++;
                }
            }
            abilityUI.UI.SetActive(true);
            abilityUI.UpdateUI(abilities);
            abilityUI.UI.SetActive(false);
        }

    }

    /// <summary>
    /// Sets active (<see langword="true"/> or <see langword="false"/>) on all abilities
    /// </summary>
    /// <param name="active"></param>
    public void SetActive(bool active){
        foreach(Ability a in abilities){
            //a.isUnlocked=active;
            a.enabled=active;
        }
    }

    public void SetActiveSingle(Ability ability, bool active)
    {
        try
        {
            FindAbility(ability.abilityName).isUnlocked = active;
            FindAbility(ability.abilityName).enabled = active;
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("ERROR: Can't set active: ability not found in manager list");
            return;
        }
    }

    public void SetActiveSingle(Ability.Abilities ability, bool active)
    {
        try
        {
            FindAbility(ability).isUnlocked = active;
            FindAbility(ability).enabled = active;
        }
        catch (System.NullReferenceException)
        {
            Debug.Log("ERROR: Can't set active: ability not found in manager list");
            return;
        }
    }

    public Ability FindAbility(Ability.Abilities name)
    {
        return abilities.Find(a => a.abilityName == name);
    }

    public bool IsUnlocked(Ability.Abilities name)
    {
        var ability = FindAbility(name);
        if (ability != null)
        {
            return ability.isUnlocked;
        }
        return false;
    }
}
