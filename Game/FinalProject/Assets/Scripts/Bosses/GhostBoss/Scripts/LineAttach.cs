using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LineAttach : MonoBehaviour
{
    [SerializeField] private LineRenderer line;
    public LineRenderer Line { get => line; }
    [SerializeField] private Transform start;
    [SerializeField] private Transform end;
    [SerializeField] private bool endPosOnGameObject;
    [SerializeField] private GameObject target;
    
    
    void Start()
    {
        if (line == null)
        {
            line = GetComponent<LineRenderer>();
        }
        line.SetPosition(0, start.position);

        if (!endPosOnGameObject)
        {
            line.SetPosition(1, end.position);
        }
        else
        {
            line.SetPosition(1, target.transform.position);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (endPosOnGameObject)
        {
            line.SetPosition(1, target.transform.position);
        }
    }
}
