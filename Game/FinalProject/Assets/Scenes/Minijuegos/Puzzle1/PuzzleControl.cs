using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MasterMinigame
{
    [SerializeField] private List<PuzzleRotation> pictures;

    public static bool puzzleCompleted;
    public static int completedCount;
    // Start is called before the first frame update
    void Start()
    {
        completedCount = 0;
        puzzleCompleted = false;

        pictures.ForEach(p => p.Rotated += Rotated);
    }


    public void Rotated()
    {
        if (!puzzleCompleted)
        {
            puzzleCompleted = pictures.TrueForAll(p => p.transform.rotation.z == 0);   
        }

        if (puzzleCompleted)
        {
            OnWinMinigame();
        }
    }
}
