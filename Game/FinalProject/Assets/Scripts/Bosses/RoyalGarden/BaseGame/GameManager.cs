
using UnityEngine;
using System.Collections.Generic;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class GameManager : MonoBehaviour
    {
        public string playerSymbol;
        public string aiSymbol;
        public string currentTurn;
        public List<Vector2[]> winningLines = new List<Vector2[]>();
        public static readonly GridLocation[,] winningGridLines = 
        {   {GridLocation.TopLeft, GridLocation.TopCenter, GridLocation.TopRight},
            {GridLocation.CenterLeft, GridLocation.CenterCenter, GridLocation.CenterRight}, 
            {GridLocation.BottomLeft, GridLocation.BottomCenter, GridLocation.BottomRight},
            {GridLocation.TopLeft, GridLocation.CenterLeft, GridLocation.BottomLeft}, 
            {GridLocation.TopCenter, GridLocation.CenterCenter, GridLocation.BottomCenter}, 
            {GridLocation.TopRight, GridLocation.CenterRight, GridLocation.BottomRight},
            {GridLocation.TopLeft, GridLocation.CenterCenter, GridLocation.BottomRight}, 
            {GridLocation.TopRight, GridLocation.CenterCenter, GridLocation.BottomLeft} };

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
            }
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
                return "I";
            }
            return string.Empty;
        }

        bool IsWinner(string symbol, Grid grid)
        {
            var squares = grid.squares;
            var symbolSquares = squares.FindAll(s => s.symbol == symbol);
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

    }
}