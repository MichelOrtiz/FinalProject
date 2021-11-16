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
            Destroy(PlayerManager.instance?.gameObject);
            Destroy(Inventory.instance?.gameObject);
            Destroy(CameraFollow.instance?.gameObject);
            if (hasTime)
            {
                Invoke("EndCredits", creditsTime);
            }
        }
        
        void Update()
        {
            if (Input.anyKeyDown)
            {
                if(busy) return;
                busy = true;
                EndCredits();
            }
        }

        void EndCredits()
        {
            SceneController.instance.RealLoasScene(sceneToLoad);
        }
    }
}