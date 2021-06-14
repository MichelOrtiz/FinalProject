using UnityEngine;
public class AbilityInter : MonoBehaviour
{
    [SerializeField] private AbilityObject abilityObject;
    [SerializeField] private byte radius;

    private Ability ability;

    private PlayerManager player;
    
    void Start()
    {
        player = PlayerManager.instance;
        ability = player.abilityManager.FindAbility(abilityObject.AbilityName);

        GetComponent<SpriteRenderer>().sprite = abilityObject.Icon;
    }

    void Update()
    {
        float distance = Vector2.Distance(player.GetPosition(), transform.position);
        if (distance <= radius)
        {
            if (Input.GetKeyDown(KeyCode.E))
            {
                
                if (ability != null)
                {
                    player.abilityManager.SetActiveSingle(ability, true);
                    Debug.Log("Obtained ability: " + ability.abilityName);

                    Destroy(gameObject);
                }

            }
        }
    }


}
