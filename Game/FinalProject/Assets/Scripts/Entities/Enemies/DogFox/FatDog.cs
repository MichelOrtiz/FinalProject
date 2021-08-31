using UnityEngine;
public class FatDog : FatType
{
    [Header("Self Additions")]
    [SerializeField] private GameObject electricField;
    [SerializeField] private State electricEffect;
    [SerializeField] private float timeBtwActive;
    private float curTimeBtwActive;
    [SerializeField] private float timeActive;
    private float curTimeActive;

    new void Start()
    {
        base.Start();
        electricField.GetComponentInChildren<CollisionHandler>().EnterTouchingContactHandler += electricField_EnterTouchingContact;
    }

    new void Update()
    {
        if (curTimeBtwActive > timeBtwActive)
        {
            if (!electricField.activeSelf) electricField.SetActive(true);
            if (curTimeActive > timeActive)
            {
                electricField.SetActive(false);
                curTimeActive = 0;
                curTimeBtwActive = 0;
            }
            else
            {
                curTimeActive += Time.deltaTime;
            }
        }
        else
        {
            curTimeBtwActive += Time.deltaTime;
        }
        base.Update();
    }

    void electricField_EnterTouchingContact(GameObject contact)
    {
        if (contact.tag == player.tag)
        {
            player.statesManager.AddState(electricEffect);
        }
    }
}