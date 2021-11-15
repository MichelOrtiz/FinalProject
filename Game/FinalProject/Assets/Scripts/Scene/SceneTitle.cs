using TMPro;
using UnityEngine;
using UnityEngine.UI;
namespace FinalProject.Assets.Scripts.Scene
{
    public class SceneTitle : MonoBehaviour
    {
        public string title;
        [SerializeField] private TMP_Text text;
        public Sprite backgroundSprite;
        void Awake()
        {
            text.text = title;
        }
    }
}