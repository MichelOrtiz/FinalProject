using UnityEngine;
using System;
namespace FinalProject.Assets.Scripts.Scene
{
    public class CreditsController : MonoBehaviour
    {
        [SerializeField] private int sceneToLoad;
        [SerializeField] private float creditsTime;


        void Start()
        {
            PlayerManager.instance?.SetEnabledPlayer(false);
            Destroy(PlayerManager.instance?.gameObject);
            Destroy(Inventory.instance?.gameObject);
            Destroy(CameraFollow.instance?.gameObject);
            Invoke("EndCredits", creditsTime);
        }
        
        void Update()
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                EndCredits();
            }
        }

        void EndCredits()
        {
            SceneController.instance.RealLoasScene(sceneToLoad);
        }
    }
}