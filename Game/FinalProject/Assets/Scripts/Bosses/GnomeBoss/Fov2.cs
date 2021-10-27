using UnityEngine;
public class Fov2 : GnomeFov
{
    protected override void Move()
    {
        transform.Translate(Vector2.left * Time.deltaTime * speedMultiplier);
    }

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
        base.Update();
    }
}
