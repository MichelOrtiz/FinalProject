using UnityEngine;
using System;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden.BaseGame
{
    public class Square : MonoBehaviour
    {
        public string symbol;
        public bool occupied;
        public GridLocation gridLocation;

        public Vector2 VectorLocation;


        /*public delegate void Clicked(Square square);
        public event Clicked ClickedHandler;
        public void OnClicked(Square square)
        {
            ClickedHandler?.Invoke(this);
        }*/
        public Action<Square> Clicked;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);

            //VectorLocation = 
        }

        public void OnClick()
        {
            //OnClicked(this);
            Clicked(this);
        }
    }
}
