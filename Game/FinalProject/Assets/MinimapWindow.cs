using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public class MinimapWindow : MonoBehaviour
    {
        public static MinimapWindow instance;
        public GameObject uI;
        private void Awake()
        {
            instance = this;
        }
        private void Update()
        {
            if (PlayerManager.instance.inputs.OpenMiniMap)
            {
                if (!uI.activeSelf)
                {
                    Show();
                }else{
                    Hide();
                }
            }
        }
        public void Show()
        {
        
            uI.SetActive(true);
        }
        public void Hide()
        {            
            uI.SetActive(false);
        }
    }
}
