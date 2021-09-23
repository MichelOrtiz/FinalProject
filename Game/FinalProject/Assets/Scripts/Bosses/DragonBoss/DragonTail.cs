using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class DragonTail : MonoBehaviour
{

    private BossFight manager;
    [SerializeField] private Item neededItem;
    [SerializeField] private CollisionHandler collisionHandler;
    
    
    void Awake()
    {
        collisionHandler.EnterTouchingContactHandler += collisionHandler_EnterContact;    
    }
    
    private void Start() 
    {
        manager=BossFight.instance;
    }
    /*protected override void Interaction()
    {
        manager.NextStage();
    }*/

    void collisionHandler_EnterContact(GameObject contact)
    {
        if (collisionHandler.Contacts.Contains(contact)) return;
        var inter = contact.GetComponentInChildren<Inter>();
        
        if (inter != null && inter.item == neededItem)
        {
            Debug.Log("next stage");
            manager.NextStage();
        }
        Destroy(contact);
    }
}
