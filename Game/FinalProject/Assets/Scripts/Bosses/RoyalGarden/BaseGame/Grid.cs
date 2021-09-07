
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class Grid : MonoBehaviour
    {

        public List<Square> squares;
        public string winner = "";
        [SerializeField] private Text winnerText;

        public bool HasWinner { get => winner != ""; }

        void Awake()
        {
            winnerText.gameObject.SetActive(false);
        }



        public void ToggleSquares(bool value)
        {
            var squares = ScenesManagers.GetComponentsInChildrenList<Square>(gameObject);
            if (squares != null)
            {
                squares.ForEach(s => s.GetComponent<Button>().enabled = value);
            }
        }

        public void SetWinner(string winner)
        {
            this.winner = winner;
            winnerText.gameObject.SetActive(true);
            winnerText.text = winner;
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