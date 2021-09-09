
using UnityEngine;
using System.Collections;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private float winDelay; 
        public string playerSymbol;
        public string aiSymbol;
        public string tieSymbol;
        public string currentTurn;
        public static readonly GridLocation[,] winningGridLines = 
        {   {GridLocation.TopLeft, GridLocation.TopCenter, GridLocation.TopRight},
            {GridLocation.CenterLeft, GridLocation.CenterCenter, GridLocation.CenterRight}, 
            {GridLocation.BottomLeft, GridLocation.BottomCenter, GridLocation.BottomRight},
            {GridLocation.TopLeft, GridLocation.CenterLeft, GridLocation.BottomLeft}, 
            {GridLocation.TopCenter, GridLocation.CenterCenter, GridLocation.BottomCenter}, 
            {GridLocation.TopRight, GridLocation.CenterRight, GridLocation.BottomRight},
            {GridLocation.TopLeft, GridLocation.CenterCenter, GridLocation.BottomRight}, 
            {GridLocation.TopRight, GridLocation.CenterCenter, GridLocation.BottomLeft} };

        public static readonly GridLocation[,] partialWinningGridLines = 
        {
            {GridLocation.TopLeft, GridLocation.TopCenter}, 
            {GridLocation.CenterLeft, GridLocation.CenterCenter},
            {GridLocation.BottomLeft, GridLocation.BottomCenter}, 
            {GridLocation.TopLeft, GridLocation.CenterLeft}, 
            {GridLocation.TopCenter, GridLocation.CenterCenter}, 
            {GridLocation.TopRight, GridLocation.CenterRight}, 
            {GridLocation.TopLeft, GridLocation.CenterCenter}, 
            {GridLocation.TopRight, GridLocation.CenterCenter},

            {GridLocation.TopCenter, GridLocation.TopRight}, 
            {GridLocation.CenterCenter, GridLocation.CenterRight}, 
            {GridLocation.BottomCenter, GridLocation.BottomRight}, 
            {GridLocation.CenterLeft, GridLocation.BottomLeft}, 
            {GridLocation.CenterCenter, GridLocation.BottomCenter}, 
            {GridLocation.CenterRight, GridLocation.BottomRight}, 
            {GridLocation.CenterCenter, GridLocation.BottomRight}, 
            {GridLocation.CenterCenter, GridLocation.BottomLeft}
        };

        void Awake()
        {
            currentTurn = playerSymbol;
        }

        public void CheckWinner(Grid grid)
        {
            var winner = GetWinner(grid);

            if (winner != "")
            {
                grid.SetWinner(winner);
                grid.ToggleSquares(false);
                //StartCoroutine(SetWinner(grid, winner));
            }
        }

        IEnumerator SetWinner(Grid grid, string symbol)
        {
            yield return new WaitForSeconds(winDelay);
            grid.SetWinner(symbol);
            grid.ToggleSquares(false);
        }

        public string GetWinner(Grid grid)
        {

            if (IsWinner(playerSymbol, grid))
            {
                return playerSymbol;
            }
            else if (IsWinner(aiSymbol, grid))
            {
                return aiSymbol;     
            }
            else if (!grid.SquaresAvailable())
            {
                return tieSymbol;
            }
            return string.Empty;
        }

        bool IsWinner(string symbol, Grid grid)
        {
            var squares = grid.squares;
            var symbolSquares = squares.FindAll(s => s.symbol == symbol || s.symbol == tieSymbol);
            for (int i = 0; i < 8; i++)
            {
                Square square = null;
                for (int j = 0; j < 3; j++)
                {
                    square = symbolSquares.Find(s => s.gridLocation == winningGridLines[i, j]);
                    if (square == null)
                    {
                        break;
                    }
                }
                if (square != null)
                {
                    return true;
                }
            }
            return false;
        }

        bool IsPartialWinner(string symbol, Grid grid)
        {
            return GetWinningSquare(symbol, grid) != null;
        }

        public Square GetWinningSquare(string symbol, Grid grid)
        {
            var squares = grid.squares;
            var symbolSquares = squares.FindAll(s => s.symbol == symbol || s.symbol == tieSymbol);
            
            for (int i = 0; i < 8; i++)
            {
                Square square = null;
                var counter = 0;
                bool squareOccupied = false;
                // default value
                GridLocation winLocation = GridLocation.TopLeft;
                for (int j = 0; j < 3; j++)
                {
                    squareOccupied = false;
                    square = symbolSquares.Find(s => s.gridLocation == winningGridLines[i, j]);
                    if (square != null)
                    {
                        counter++;
                    }
                    else
                    {
                        var location = winningGridLines[i, j];
                        if (!squares.Find(s => s.gridLocation == location).occupied)
                        {
                            winLocation = location;
                        }
                        else
                        {
                            squareOccupied = true;
                        }
                    }
                }
                if (counter == 2 && !squareOccupied)
                {
                    if (!squares.Find(s => s.gridLocation == winLocation).occupied)
                    {
                        return squares.Find(s => s.gridLocation == winLocation);
                    }
                }
            }
            return null;
        }

    }
}