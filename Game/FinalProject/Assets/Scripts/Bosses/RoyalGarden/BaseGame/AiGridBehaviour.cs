using UnityEngine;
using System.Collections;
namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class AiGridBehaviour : MonoBehaviour
    {
        public string symbol;
        private GridController gridController;
        [SerializeField] private BoardController boardController;
        [SerializeField] private float delay;
        public void TakeTurn()
        {
            var squares = boardController.currentGrid.GetAvailableSquares();
            gridController = boardController.currentGridControler;
            if (boardController.currentGrid.SquaresAvailable())
            {
                var square = RandomGenerator.RandomElement<Square>(squares);
                StartCoroutine(SelectSquare(square));
            }
        }

        private IEnumerator SelectSquare(Square square)
        {
            yield return new WaitForSeconds(delay);
            gridController.SelectSquare(square, symbol);
            boardController.OnSquareClick(square);
        }
    }
}