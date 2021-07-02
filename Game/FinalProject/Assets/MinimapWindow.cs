using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public class MinimapWindow : MonoBehaviour
    {
        public static MinimapWindow instance;
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (instance==null)
            {
                instance = this;
            }
        }
        public void Show()
        {
            instance?.gameObject?.SetActive(true);
        }
        public void Hide()
        {            
            instance?.gameObject?.SetActive(false);
        }
    }
}
