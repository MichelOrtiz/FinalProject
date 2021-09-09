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
        public Action<Square> Clicked;
        void Start()
        {
            GetComponent<Button>()?.onClick.AddListener(OnClick);
        }

        public void OnClick()
        {
            Clicked?.Invoke(this);
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
