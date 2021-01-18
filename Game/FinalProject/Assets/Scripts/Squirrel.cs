using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Squirrel : Enemy
{
    public bool movingRight;
    private int yAngle;
    

    new void Start()
    {
        //rigidbody2D = GetComponent<Rigidbody2D>();
        base.Start();
    }

    // Update is called once per frame
    void Update()
    {
        collissionInfo = Physics2D.Raycast(collisionChecker.position, Vector2.down, checkRadius);
        inFrontOfObstacle = !collissionInfo.collider;
        UpdateAnimationState();
        StartCoroutine(MainRoutine());
    }


    public override IEnumerator MainRoutine()
    {
        if (inFrontOfObstacle)
        {
            yAngle = movingRight? 180: 0;
            yield return new WaitForSeconds(2);
            transform.eulerAngles = new Vector3(0, yAngle, 0);
            movingRight = !movingRight;
        }
        else
        {
            transform.Translate(Vector3.left * Time.deltaTime * normalSpeed);
        }
    }

    protected override void UpdateAnimationState()
    {
        animator.SetBool("Is Walking", !inFrontOfObstacle);
    }
}
