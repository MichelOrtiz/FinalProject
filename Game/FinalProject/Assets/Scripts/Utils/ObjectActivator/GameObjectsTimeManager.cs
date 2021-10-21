using UnityEngine;
using System.Collections.Generic;

namespace FinalProject.Assets.Scripts.Utils.ObjectActivator
{
    public class GameObjectsTimeManager : MonoBehaviour
    {
        [SerializeField] private List<GameObjectTime> gameObjects;

        void Awake()
        {
            foreach (var got in gameObjects)
            {
                var activator = gameObject.AddComponent<GameObjectTimeActivator>();
                activator.gameObjectTime = got;
            }            
        }
    }
}