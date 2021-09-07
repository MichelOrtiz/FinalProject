using UnityEngine;
namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden.BaseGame
{
    public class AiGridBehaviour : MonoBehaviour
    {
        public string symbol;
        [SerializeField] private GridController gridController;
        public void TakeTurn()
        {
            var squares = gridController.GetAvailableSquares();
            if (squares != null && squares.Count > 0)
            {
                gridController.SelectSquare(RandomGenerator.RandomElement<Square>(squares), symbol);
            }
        }
    }
}