using UnityEngine;
public class Fov3 : GnomeFov
{
    [SerializeField] private Transform center;
    [SerializeField] private  float radius;

    private Vector2 endPoint;
    private float angle =0;
    private float speed=(2*Mathf.PI)/4 ; //2*PI in degress is 360, so you get 4 seconds to complete a circle

    private enum Direction
    {
        Clockwise, 
        CounterClock
    }
    private Direction direction = Direction.Clockwise;

    protected override void Move()
    {
        if (direction == Direction.Clockwise)
        {
            angle -= speed*Time.deltaTime;
        }
        else
        {
            angle += speed*Time.deltaTime;
        }
        endPoint.x = Mathf.Cos(angle)*radius+center.position.x;
        endPoint.y = Mathf.Sin(angle)*radius+center.position.y;
        mesh.transform.position = endPoint;
        Debug.DrawLine(center.position, endPoint, Color.red);
        return;
    }
    
    new void Update()
    {
        if (!justAttacked)
        {
            Move();
        }
        else if (timeAfterAttack > baseTimeAfterAttack)
        {

            ChangeDirection();
            endPoint.x = Mathf.Cos(90)*radius+center.position.x;
            endPoint.y = Mathf.Sin(90)*radius+center.position.y;
        }
        base.Update();
    }

    private void ChangeDirection()
    {
        if (direction == Direction.Clockwise)
        {
            direction = Direction.CounterClock;
        }
        else
        {
            direction = Direction.Clockwise;
        }
    }
}
