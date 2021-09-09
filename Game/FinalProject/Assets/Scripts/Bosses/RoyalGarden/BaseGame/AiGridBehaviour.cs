using UnityEngine;
using System.Collections;
using System.Collections.Generic;
namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class AiGridBehaviour : MonoBehaviour
    {
        public string symbol { get; private set; }
        private GridController gridController;
        [SerializeField] private BoardController boardController;
        [SerializeField] private float delay;
        [SerializeField] private GameManager gameManager;

        private Dictionary<string, short> scores;
        
        void Awake()
        {
            symbol = gameManager.aiSymbol;
            scores = new Dictionary<string, short>
            {
                {gameManager.aiSymbol, 1},
                {gameManager.tieSymbol, 0}, 
                {gameManager.playerSymbol, -1}
            };
        }

        public void TakeTurn()
        {
            if (boardController.mainGrid.HasWinner) return;

            var currentGrid = boardController.currentGrid;
            var avSquares = currentGrid.GetAvailableSquares();
            gridController = boardController.currentGridController;
            /*var bestScore = -2;
            Square bestSquare = RandomGenerator.RandomElement<Square>(avSquares);*/
            if (boardController.currentGrid.SquaresAvailable())
            {
                /*foreach (var square in avSquares)
                {
                    //var tGrid = CreateGridCopy(gridController.grid);
                    //var selSquare = tGrid.squares.Find(s => s.gridLocation == square.gridLocation);

                    square.SetSymbol(symbol);

                    //var tController = CreateGridControllerCopy(gridController, tGrid);
                    //gridController.SelectSquare(square, symbol);
                    var score = Minimax(currentGrid, 1, false);
                    square.RemoveSymbol();
                    //Destroy(tGrid);
                    //Destroy(tController);
                    if (score > bestScore)
                    {
                        bestScore = score;
                        bestSquare = square;
                    }
                }
                gridController.SelectSquare(bestSquare, symbol);
                boardController.OnPlayedTurn(bestSquare);*/


                var square = RandomGenerator.RandomElement<Square>(avSquares);
                StartCoroutine(SelectSquare(square));

            }
        }

        private IEnumerator SelectSquare(Square square)
        {
            yield return new WaitForSeconds(delay);
            gridController.SelectSquare(square, symbol);
            boardController.OnPlayedTurn(square);
        }


        int Minimax(Grid grid, int depth, bool isMaximizing)
        {
            if (gameManager.GetWinner(grid) != string.Empty)
            {
                return scores[gridController.grid.winner];
            }
            if (isMaximizing)
            {
                int bestScore = -2;
                var avSquares = gridController.grid.GetAvailableSquares();
                if (grid.SquaresAvailable())
                {
                    foreach (var square in avSquares)
                    {
                        square.SetSymbol(symbol);
                        var score = Minimax(grid, depth + 1, false);
                        square.RemoveSymbol();
                        bestScore = Mathf.Max(score, bestScore);
                    }
                }
                return bestScore;
            }
            else
            {
                int bestScore = 2;
                var avSquares = gridController.grid.GetAvailableSquares();
                if (grid.SquaresAvailable())
                {
                    foreach (var square in avSquares)
                    {
                        square.SetSymbol(gameManager.playerSymbol);
                        var score = Minimax(grid, depth + 1, true);
                        square.RemoveSymbol();
                        bestScore = Mathf.Min(score, bestScore);
                    }
                }
                return bestScore;
            }
        }

        /*Grid CreateGridCopy(Grid grid)
        {
            var squares = grid.squares;
            var tGrid = Instantiate(grid);
            foreach (var square in squares)
            {
                var tSquare = Instantiate(square);
                tGrid.squares.Add(tSquare);
            }
            return tGrid;
        }

        GridController CreateGridControllerCopy(GridController gridController, Grid grid)
        {
            var tController = Instantiate(gridController);
            tController.grid = grid;
            return tController;
        }*/
    }
}