using UnityEngine;
using System;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class Square : MonoBehaviour
    {
        public string symbol;
        public bool occupied;
        public GridLocation gridLocation;
        [SerializeField] private Text text;


        /*public delegate void Clicked(Square square);
        public event Clicked ClickedHandler;
        public void OnClicked(Square square)
        {
            ClickedHandler?.Invoke(this);
        }*/
        public Action<Square> Clicked;
        void Start()
        {
            GetComponent<Button>()?.onClick.AddListener(OnClick);

            //VectorLocation = 
        }

        public void OnClick()
        {
            //OnClicked(this);
            Clicked(this);
        }

        public void SetSymbol(string symbol)
        {
            if (!transform.parent.GetComponent<Grid>().HasWinner)
            {
                this.symbol = symbol;
                if (text != null)
                {
                    text.text = symbol;
                }
                occupied = true;
            }
            
        }

        public void RemoveSymbol()
        {
            symbol = "";
            if (text != null)
            {
                text.text = symbol;
            }
            occupied = false;
        }
    }
}
