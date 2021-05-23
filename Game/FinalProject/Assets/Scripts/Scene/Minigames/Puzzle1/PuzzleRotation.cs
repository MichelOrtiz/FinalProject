using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleRotation : MonoBehaviour
{
    
    private void OnMouseDown()
    {
        if(!PuzzleControl.puzzleCompleted){
            transform.Rotate(0f, 0f, 90f);
        }
    }
}
