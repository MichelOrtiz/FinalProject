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
        [SerializeField] private GridController gridController;
        [SerializeField] private Grid mainGrid;
        [SerializeField] private UIBossFight bossFight;
        public Grid currentGrid;
        public GridController currentGridControler;
        

        void Start()
        {
            foreach (var grid in grids)
            {
                //grid.squares.ForEach(s => s.Clicked += OnSquareClick);
                grid.GetComponent<GridController>().AiTurn += aiGridBehaviour.TakeTurn;
                grid.GetComponent<GridController>().PlayedTurn += OnPlayedTurn;

                grid.Finished += grid_Finished;
            }
        }


        List<Grid> GetAvailableGrids()
        {
            return grids.FindAll(g => !g.HasWinner);
        }

        public void OnPlayedTurn(Square square)
        {
            var avGrids = GetAvailableGrids();
            Grid tempGrid = null;

            gameManager.CheckWinner(mainGrid);

            if (mainGrid.HasWinner)
            {
                if (mainGrid.winner == gameManager.playerSymbol)
                {
                    bossFight.EndBattle();
                }
                else
                {
                    bossFight.LooseBattle();
                }
                return;
            }


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
            else if(avGrids != null && avGrids.Count > 0)
            {
                avGrids.ForEach( g => g.ToggleSquares(true));

                if (gameManager.currentTurn == gameManager.playerSymbol)
                {
                    currentGrid = RandomGenerator.RandomElement<Grid>(avGrids);
                    currentGridControler = currentGrid.GetComponent<GridController>();
                }
            }
        }


        void grid_Finished(Grid grid)
        {
            grid.GetComponent<Square>().SetSymbol(grid.winner);
            //gameManager.CheckWinner(grid);
        }
    }
}