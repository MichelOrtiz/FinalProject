using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(CollisionHandler))]
public class DarknessScript : MonoBehaviour
{
    [HideInInspector] public GameObject Oscuridad;

    [Tooltip("To activate the darkness by this script, using the collider.")]
    [SerializeField] private bool selfActivate;
    public GameObject ObscureLight;
    public GameObject LightLight;

    private CollisionHandler collisionHandler;

    private GameObject playerCollision;

    PlayerManager player;
    Collider2D collision;

    public Action ExitDarkness;
    

    void Awake()
    {
        collisionHandler = GetComponent<CollisionHandler>();

        if (selfActivate)
        {
            collisionHandler.ExitTouchingContactHandler += collisionHandler_EnterContact;
            collisionHandler.StayTouchingContactHandler += collisionHandler_EnterContact;
        }
        collisionHandler.ExitTouchingContactHandler += collisionHandler_ExitContact;

    }

    // Start is called before the first frame update
    void Start()
    {
        player = PlayerManager.instance;
        playerCollision = player.collisionHandler.gameObject;

        Oscuridad = player.Darkness;

        player.isInDark = false;
        Oscuridad.SetActive(false);
    }

    public void SetActiveDarkness(bool value)
    {
        if (GetComponent<CollisionHandler>() == null) return;
        if (collisionHandler.TouchingContact(playerCollision))
        {
            player.isInDark = value;
            Oscuridad.SetActive(value);

            ObscureLight.SetActive(value);
            LightLight.SetActive(!value);
        }
    }


    void collisionHandler_EnterContact(GameObject contact)
    {
        if (contact == playerCollision)
        {
            SetActiveDarkness(true);
        }
    }

    void collisionHandler_StayContact(GameObject contact)
        {
            if (contact == playerCollision)
            {
                SetActiveDarkness(true);
            }
        }

    void collisionHandler_ExitContact(GameObject contact)
    {
        if (contact == playerCollision)
        {
            SetActiveDarkness(false);
            ExitDarkness?.Invoke();
        }
    }

    private void OnDestroy()
    {
        try
        {
             
        Oscuridad?.SetActive(false);  
        }
        catch (System.Exception)
        {
            
        }  
    }

    /*private void OnTriggerEnter2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            SetActiveDarkness(true);
        }
    }

    private void OnTriggerStay2D(Collider2D collision){    
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            SetActiveDarkness(true);
        }
    }

    private void OnTriggerExit2D(Collider2D collision){
        GameObject collisionGameObject = collision.gameObject;
        if (collisionGameObject.tag == "Player")
        {
            SetActiveDarkness(false);
        }
    }*/
}
