using UnityEngine;
namespace FinalProject.Assets.Scripts.Utils.ObjectActivator
{
    public class GameObjectTimeActivator : MonoBehaviour
    {
        public GameObjectTime gameObjectTime;

        void Start()
        {
            InvokeRepeating("ToggleActive", 0.1f, gameObjectTime.time);
        }

        void ToggleActive()
        {
            gameObjectTime.gameObject.SetActive(!gameObjectTime.gameObject.activeSelf);
        }
    }
}