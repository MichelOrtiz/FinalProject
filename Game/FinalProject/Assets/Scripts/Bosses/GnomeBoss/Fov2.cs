using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov2 : GnomeFov
{
    protected override void Move()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speedMultiplier);
    }

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    
}
