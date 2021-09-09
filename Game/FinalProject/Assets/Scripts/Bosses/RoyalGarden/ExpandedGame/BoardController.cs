using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

namespace FinalProject.Assets.Scripts.Bosses.RoyalGarden
{
    public class BoardController : MonoBehaviour
    {
        [SerializeField] private List<Grid> grids;
        [SerializeField] private GameManager gameManager;
        [SerializeField] private AiGridBehaviour aiGridBehaviour;
        [SerializeField] private GridController gridController;
        public Grid mainGrid;
        [SerializeField] private UIBossFight bossFight;
        [SerializeField] private Color highlightColor;
        [SerializeField] private Color defaultColor;
        public Grid currentGrid;
        public GridController currentGridController;
        
        [SerializeField] private float endDelay;

        void Start()
        {
            foreach (var grid in grids)
            {
                //grid.squares.ForEach(s => s.Clicked += OnSquareClick);
                grid.GetComponent<GridController>().AiTurn += aiGridBehaviour.TakeTurn;
                grid.GetComponent<GridController>().PlayedTurn += OnPlayedTurn;

                grid.Finished += grid_Finished;
            }
            HighlightAvailableGrids();
        }


        List<Grid> GetAvailableGrids()
        {
            return grids.FindAll(g => !g.HasWinner);
        }

        public void OnPlayedTurn(Square square)
        {
            // To only use unfinished grids
            var avGrids = GetAvailableGrids();
            Grid tempGrid = null;

            gameManager.CheckWinner(mainGrid);

            if (mainGrid.HasWinner)
            {
                StartCoroutine(SetMainWinner());
                return;
            }


            foreach (var grid in avGrids)
            {
                // finds the location of the next grid based on the relative location of the square
                if(grid.GetComponent<Square>().gridLocation == square.gridLocation)
                {
                    tempGrid = grid;
                    break;
                }
            }

            // The grid has been found
            if (tempGrid != null)
            {
                currentGrid = tempGrid;
                currentGridController = currentGrid.GetComponent<GridController>();
                currentGrid.ToggleSquares(true);
                grids.FindAll(g => g != currentGrid).ForEach(g => g.ToggleSquares(false));
            }
            else if(avGrids != null && avGrids.Count > 0)
            {
                // Enables all available grids
                avGrids.ForEach( g => g.ToggleSquares(true));

                if (gameManager.currentTurn == gameManager.aiSymbol)
                {
                    currentGrid = RandomGenerator.RandomElement<Grid>(avGrids);
                    currentGridController = currentGrid.GetComponent<GridController>();
                }
            }
            HighlightAvailableGrids();
        }

        IEnumerator SetMainWinner()
        {
            yield return new WaitForSeconds(endDelay);
            if (mainGrid.winner == gameManager.playerSymbol)
            {
                bossFight.EndBattle();
            }
            else
            {
                bossFight.LooseBattle();
            }
        }

        void grid_Finished(Grid grid)
        {
            grid.GetComponent<Square>().SetSymbol(grid.winner);
            //gameManager.CheckWinner(grid);
        }

        void HighlightAvailableGrids()
        {
            var avGrids = grids.FindAll( g => g.ButtonSquaresEnabled());
            var unavGrids = grids.FindAll( g => !g.ButtonSquaresEnabled());
            if (avGrids != null)
            {
                avGrids.ForEach( g => g.gameObject.GetComponent<Image>().color = highlightColor);
            }

            if (unavGrids != null)
            {
                unavGrids.ForEach(g => g.gameObject.GetComponent<Image>().color = defaultColor);
            }
            
        }
    }
}