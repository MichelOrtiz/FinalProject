using UnityEngine;
public class SeekerGhost : Ghost
{
    [SerializeField] private Transform dividePos; 
    [SerializeField] private float baseStartTimeUntilDivide;
    [SerializeField] private float baseMainTimeUntilDivide;
    [SerializeField] private int maxGhosts;

    private bool alreadyDivided;
    private float startTimeUntilDivide;
    private float mainTimeUntilDivide;
    private int ghostsDivisions;

    new void Start()
    {
        base.Start();
    }

    new void Update()
    {
        ghostsDivisions = ScenesManagers.GetObjectsOfType<SeekerGhost>().Count;
        if (ghostsDivisions == 0)
        {
            if (startTimeUntilDivide < baseStartTimeUntilDivide)
            {
                startTimeUntilDivide += Time.deltaTime;
            }
            else
            {
                Divide();
            }
        }
        else
        {
            if (mainTimeUntilDivide < baseMainTimeUntilDivide)
            {
                mainTimeUntilDivide += Time.deltaTime;
            }
            else
            {
                Divide();
                mainTimeUntilDivide = 0;
            }
        }
        
        base.Update();
    }

    protected override void MainRoutine()
    {
        rigidbody2d.Sleep();
    }

    protected override void ChasePlayer()
    {
        rigidbody2d.position = Vector3.MoveTowards(GetPosition(), player.GetPosition(), chaseSpeed * Time.deltaTime);
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
    }

    private void Divide()
    {
        if (!alreadyDivided)
        {
            if (ghostsDivisions < maxGhosts)
            {
                Instantiate(this, dividePos.position, Quaternion.identity).baseStartTimeUntilDivide = 5f;
                alreadyDivided = true;
                //ghostsDivisions++;
            }
        }
    }
}