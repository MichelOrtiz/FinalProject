using UnityEngine;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private List<Grid> grids;
        [SerializeField] private List<Square> boardSquares;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private AiGridBehaviour aiGridBehaviour;
        public Grid currentGrid;
        public GridController currentGridControler;
        

        void Start()
        {
            foreach (var grid in grids)
            {
                //grid.squares.ForEach(s => s.Clicked += OnSquareClick);
                grid.GetComponent<GridController>().AiTurn += aiGridBehaviour.TakeTurn;
                grid.GetComponent<GridController>().PlayedTurn += OnSquareClick;
            }
        }


        List<Grid> GetAvailableGrids()
        {
            return grids.FindAll(g => !g.HasWinner);
        }

        public void OnSquareClick(Square square)
        {
            /*if (currentGrid == null)
            {
                currentGrid = square.transform.parent.GetComponent<Grid>();
                currentGridControler = currentGrid.GetComponent<GridController>();
                return;
            }*/

            var avGrids = GetAvailableGrids();
            Grid tempGrid = null;

            foreach (var grid in avGrids)
            {
                if(grid.GetComponent<Square>().gridLocation == square.gridLocation)
                {
                    tempGrid = grid;
                    
                }
            }

            if (tempGrid != null)
            {
                currentGrid = tempGrid;
                currentGridControler = currentGrid.GetComponent<GridController>();
                currentGrid.ToggleSquares(true);
                grids.FindAll(g => g != currentGrid).ForEach(g => g.ToggleSquares(false));
            }
            else
            {
                avGrids.ForEach( g => g.ToggleSquares(true));

                if (gameManager.currentTurn == gameManager.aiSymbol)
                {
                    currentGrid = RandomGenerator.RandomElement<Grid>(avGrids);
                    currentGridControler = currentGrid.GetComponent<GridController>();
                }
            }

            /*if (tempGrid == null && avGrids != null && avGrids.Count > 0)
            {
                tempGrid = RandomGenerator.RandomElement<Grid>(avGrids);
            }

            if (tempGrid != null)
            {
                currentGrid = tempGrid;
                currentGridControler = currentGrid.GetComponent<GridController>();
                currentGrid.ToggleSquares(true);
                grids.FindAll(g => g != currentGrid).ForEach(g => g.ToggleSquares(false));
            }
            else
            {
                Debug.Log("the end");
            }*/
        }

    }
}