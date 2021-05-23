using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PuzzleControl : MonoBehaviour
{
    [SerializeField] private Transform[] pictures;

    public static bool puzzleCompleted;
    // Start is called before the first frame update
    void Start()
    {
        puzzleCompleted = false; 
    }

    // Update is called once per frame
    void Update()
    {
        for (int i = 0; i < 36; i++){
            if(pictures[i].rotation.z != 0){
                puzzleCompleted = false;
            }else{
                puzzleCompleted = true;
            }
        }
    }
}
