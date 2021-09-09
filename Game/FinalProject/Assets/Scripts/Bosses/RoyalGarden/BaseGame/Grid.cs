
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;
using System;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class Grid : MonoBehaviour
    {

        public List<Square> squares;
        public string winner = "";
        [SerializeField] private Text winnerText;

        public bool HasWinner { get => winner != ""; }

        public Action<Grid> Finished;

        void Awake()
        {
            winnerText.gameObject.SetActive(false);
            if (squares == null)
            {
                squares = new List<Square>();
            }
        }



        public void ToggleSquares(bool value)
        {
            var squares = ScenesManagers.GetComponentsInChildrenList<Square>(gameObject);
            if (squares != null)
            {
                foreach (var square in squares)
                {
                    if (square.TryGetComponent<Button>(out var bt))
                    {
                        bt.enabled = value;
                    }
                }
            }
        }

        public void SetWinner(string winner)
        {
            this.winner = winner;
            winnerText.gameObject.SetActive(true);
            winnerText.text = winner;
            Finished?.Invoke(this);
        }

        public bool SquaresAvailable()
        {
            var squares = GetAvailableSquares();
            return squares != null && squares.Count > 0;
        }

        public List<Square> GetAvailableSquares()
        {
            var avSquares = squares.FindAll(s => !s.occupied);
            return avSquares;
        }
    }

}