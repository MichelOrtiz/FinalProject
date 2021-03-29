using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fov1 : GnomeFov
{
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

    protected override void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speedMultiplier);
    }
}