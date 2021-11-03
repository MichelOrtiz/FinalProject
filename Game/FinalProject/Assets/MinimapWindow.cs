using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public class MinimapWindow : MonoBehaviour
    {
        public static MinimapWindow instance;
        public GameObject UI;
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (PlayerManager.instance.inputs.OpenMiniMap)
            {
                if (!UI.activeSelf)
                {
                    Show();
                }else{
                    Hide();
                }
            }
        }
        public void Show()
        {
        
            UI.SetActive(true);
        }
        public void Hide()
        {            
            UI.SetActive(false);
        }
    }
}
