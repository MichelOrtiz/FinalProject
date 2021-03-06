using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
public class DragAndDrop : MasterMinigame
{
   public GameObject SelectedPiece;
   public GameObject LastSelectedPiece;
   int OrderInLayer = 1;
   [SerializeField]int ganar=0;
    void Start()
    {
        
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
        if(LastSelectedPiece.GetComponent<PiceseScript>().correcta){
            ganar++;
            LastSelectedPiece = null;
            if (ganar==36)
            {
                OnWinMinigame();
            }
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
}
