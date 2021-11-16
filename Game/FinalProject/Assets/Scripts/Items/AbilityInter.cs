using UnityEngine;
public class AbilityInter : MonoBehaviour
{
    [SerializeField] private AbilityObject abilityObject;
    [SerializeField]private Ability.Abilities abilityName;
    private Ability ability;
    [SerializeField] private PopUpTrigger abilityPopUp;
    [SerializeField] private byte radius;

    [Header("FX")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private GameObject bubble;


    private PlayerManager player;
    
    void Start()
    {
        player = PlayerManager.instance;
        ability = player.abilityManager.FindAbility(abilityName);

        //GetComponent<SpriteRenderer>().sprite = abilityObject.Icon;
        spriteRenderer.sprite = ability.iconAbility;
    
        if (player.abilityManager.IsUnlocked(abilityName))
        {
            Destroy(gameObject);
        }    
    
    }

    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(), transform.position);
        if (distance <= radius)
        {
            bubble.SetActive(true);
            if (Input.GetKeyDown(player.inputs.controlBinds["MENUINTERACTION"]))
            {
                if (ability != null)
                {
                    player.abilityManager.SetActiveSingle(ability, true);
                    abilityPopUp.popUp.Message = abilityName.ToString();
                    abilityPopUp.TriggerPopUp(true);
                    Destroy(gameObject);
                }
            }
        }
        bubble.SetActive(false);
    }


}
