using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden.BaseGame
{
    public class GridController : MonoBehaviour
    {
        public string playerSymbol;
        public string aiSymbol;

        
        public string currentTurn;

        [SerializeField] private AiGridBehaviour aiGridBehaviour;

        public Grid grid;


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
            SetWinningLines();
        }

        void Start()
        {
            currentTurn = playerSymbol;

            foreach (var square in grid.squares)
            {
                square.Clicked += OnSquareClick;
            }
        }

        void SetWinningLines()
        {
            /*Vector2[] row = new Vector2[3];
            Vector2[] column = new Vector2[3];
            for (int i = 0; i < 3; i++)
            {
                for (int j = 0; j < 3; j++)
                {
                    row[j] = new Vector2(i, j);
                    column[j] = new Vector2(j, i);
                    Debug.Log("row: " + j + ": " + row[j]);
                }
                winningLines.Add(row);
                winningLines.Add(column);
            }*/

            Vector2[] topHor = { new Vector2(0, 0), new Vector2(0, 1), new Vector2(0, 2)};

            Vector2[] diagonal1 = {
                new Vector2(0, 0), new Vector2(1, 1), new Vector2(2, 2),
            };

            Vector2[] diagonal2 = {
                new Vector2(0, 2), new Vector2(1, 1), new Vector2(2, 0)
            };

            winningLines.Add(topHor);

            winningLines.Add(diagonal1);
            winningLines.Add(diagonal2);
        }


        public void SelectSquare(Square square, string symbol)
        {
            square.occupied = true;
            square.symbol = symbol;

            square.GetComponentInChildren<Text>().text = symbol;

            if (SquaresAvailable() && !CheckWinner())
            {
                NextTurn();
            }
        }

        public void OnSquareClick(Square square)
        {
            // Checks if it's Player's turn
            if (currentTurn == playerSymbol && !square.occupied)
            {
                SelectSquare(square, playerSymbol);
            }
        }

        public void NextTurn()
        {
            currentTurn = currentTurn == playerSymbol? aiSymbol : playerSymbol;

            if (currentTurn == aiSymbol)
            {
                aiGridBehaviour.TakeTurn();
            }
        }

        public List<Square> GetAvailableSquares()
        {
            var squares = grid.squares.FindAll(s => !s.occupied);
            return squares;
        }

        public bool SquaresAvailable()
        {
            var squares = GetAvailableSquares();
            return squares != null && squares.Count > 0;
        }

        public bool CheckWinner()
        {
            var squares = grid.squares;
            
            var playerSquares = squares.FindAll(s => s.symbol == playerSymbol);
            var aiSquares = squares.FindAll(s => s.symbol == aiSymbol);

            int index = 0;
            for (int i = 0; i < 8; i++)
            {
                Square square = null;
                for (int j = 0; j < 3; j++)
                {
                    square = playerSquares.Find(s => s.gridLocation == winningGridLines[i, j]);
                    if (square == null)
                    {
                        break;
                    }
                    else
                    {
                        Debug.Log("found some location: " + square.name);
                    }
                }
                if (square != null)
                {
                    Debug.Log("player won");
                    return true;
                }
            }
            return false;
        }
    }
}