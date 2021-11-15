using System;
using UnityEngine;
using UnityEngine.EventSystems;
public class PuzzleRotation : MonoBehaviour, IPointerDownHandler
{
    [SerializeField] private PuzzleControl puzzleControl;
    public Action Rotated;

    public void OnPointerDown(PointerEventData eventData)
    {
        Debug.Log("rotate " + gameObject);
        if(!PuzzleControl.puzzleCompleted){
            transform.Rotate(0f, 0f, 90f);

            // invokes the method assigned to the action
            Rotated?.Invoke();
        }
    }
}
