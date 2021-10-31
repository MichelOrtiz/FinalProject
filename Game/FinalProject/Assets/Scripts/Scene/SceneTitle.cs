using TMPro;
using UnityEngine;
namespace FinalProject.Assets.Scripts.Scene
{
    public class SceneTitle : MonoBehaviour
    {
        public string title;
        [SerializeField] private TMP_Text text;
        void Awake()
        {
            text.text = title;
        }

        
    }
}