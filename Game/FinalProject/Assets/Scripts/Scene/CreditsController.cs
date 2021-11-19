using UnityEngine;
using System;
namespace FinalProject.Assets.Scripts.Scene
{
    public class CreditsController : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad;

        [SerializeField] private bool hasTime;
        [SerializeField] private float creditsTime;
        bool busy;

        void Start()
        {
            busy = false;
            PlayerManager.instance?.SetEnabledPlayer(false);
            if(PlayerManager.instance != null)  Destroy(PlayerManager.instance?.gameObject);
            if(Inventory.instance != null) Destroy(Inventory.instance?.gameObject);
            if(CameraFollow.instance != null) Destroy(CameraFollow.instance?.gameObject);
            
            if (hasTime)
            {
                Invoke("EndCredits", creditsTime);
            }
        }
        
        void Update()
        {
            if (Input.anyKeyDown)
            {
                EndCredits();
            }
        }

        void EndCredits()
        {
            if(busy) return;
            busy = true;
            SceneController.instance.RealLoasScene(sceneToLoad);
        }


    }
}