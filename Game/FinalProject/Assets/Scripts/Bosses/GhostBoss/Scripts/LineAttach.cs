using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttach : MonoBehaviour
{
    private LineRenderer line;
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    void Start()
    {
        line = GetComponent<LineRenderer>();
        line.SetPosition(0, start.position);
        line.SetPosition(1, end.position);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
