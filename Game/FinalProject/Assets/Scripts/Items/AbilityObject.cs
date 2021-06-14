using UnityEngine;

[CreateAssetMenu(fileName="New AbilityObject", menuName = "Abilities/Ability object")]
public class AbilityObject : ScriptableObject
{
    [SerializeField] private Ability.Abilities abilityName;
    public Ability.Abilities AbilityName { get => abilityName; }
    
    [TextArea(3, 10)]
    [SerializeField] private string description;
    public string Description { get => description; }
    
    [SerializeField] private Sprite icon;
    public Sprite Icon { get => icon; }



}
