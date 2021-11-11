using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Minimap{
    public class MinimapWindow : MonoBehaviour
    {
        public static MinimapWindow instance;
        public GameObject UI;
        public Animator animator;
        bool isOpen;
        private void Awake()
        {
            instance = this;
            isOpen = true;
        }
        private void Start() {
            animator.SetBool("IsOpen",true);
            isOpen = true;
        }
        private void Update()
        {
            if (PlayerManager.instance.inputs.OpenMiniMap)
            {
                isOpen = !isOpen;
                if (isOpen)
                {
                    Show();
                }else{
                    Hide();
                }
            }
        }
        public void Show()
        {
            animator.SetBool("IsOpen",true);
            //UI.SetActive(true);
        }
        public void Hide()
        {        
            animator.SetBool("IsOpen",false);    
            //UI.SetActive(false);
        }
    }
}
