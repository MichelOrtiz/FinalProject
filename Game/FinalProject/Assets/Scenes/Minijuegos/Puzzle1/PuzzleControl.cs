using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    [SerializeField] private Transform[] pictures;

    public static bool puzzleCompleted;
    public static int completedCount;
    // Start is called before the first frame update
    void Start()
    {
        completedCount = 0;
        puzzleCompleted = false;
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 36; i++){
            if(pictures[i].rotation.z == 0){
                completedCount ++;
            }
        }
        if(completedCount == 36){
            Debug.Log("Puzzle Completed");
            puzzleCompleted = true;
        }else{
            puzzleCompleted = false;
        }
    }
}
