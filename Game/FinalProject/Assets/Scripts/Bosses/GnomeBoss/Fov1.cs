using UnityEngine;

public class Fov1 : GnomeFov
{
    // Update is called once per frame
    new void Update()
    {
        if (!justAttacked)
        {
            if (!IsNearEdge())
            {
                Move();
            }
            else
            {
                if (timeUntilChange > baseTimeUntilChange)
                {
                    ChangePosition();
                    timeUntilChange = 0;
                }
                else
                {
                    timeUntilChange += Time.deltaTime;
                }
            }
        }
        Debug.DrawLine(groundCheck.position, groundCheck.position - groundCheck.up * 1f, Color.black);
        
        base.Update();
    }

    protected override void Move()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speedMultiplier);
    }
}