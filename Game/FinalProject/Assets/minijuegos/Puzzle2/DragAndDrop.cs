using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class DragAndDrop : MasterMinigame
{
   public GameObject SelectedPiece;
   public GameObject LastSelectedPiece;
   public GameObject piecesD;
   int OrderInLayer = 1;
   [SerializeField]int ganar=0;
    new void Start()
    {
        base.Start();
        SelectedPiece = null;
        LastSelectedPiece = null;
        //WinMinigameHandler += CosasAlGanar;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonUp(0))
        {
            if (SelectedPiece != null)
            {    
                LastSelectedPiece=SelectedPiece;
                SelectedPiece.GetComponent<PiceseScript>().Selected = false;
                SelectedPiece = null;   
            }
        }
        if (SelectedPiece != null)
        {
            Vector2 MousePoint = Input.mousePosition;
            SelectedPiece.transform.position = new Vector2(MousePoint.x, MousePoint.y);
        }
        if(LastSelectedPiece != null && LastSelectedPiece.GetComponent<PiceseScript>().correcta){
            ganar++;
            LastSelectedPiece = null;
            Debug.Log(ganar);
        }
        if (ganar==36)
        {
            Debug.Log("ganaste");
            OnWinMinigame();
        }
    }

    public void presionado(GameObject x){
        if(x.transform.CompareTag("Puzzle")){
            if (!x.transform.GetComponent<PiceseScript>().InRightPosition)
            {
                SelectedPiece = x.transform.gameObject;
                SelectedPiece.GetComponent<PiceseScript>().Selected = true;
                SelectedPiece.GetComponent<SortingGroup>().sortingOrder  = OrderInLayer;
                OrderInLayer++;
            }
        }
    }
    protected override void AboutToEnd(){
        piecesD.SetActive(false);
    }
}
