using UnityEngine;
public class ElectricMedufin : Medufin
{
    new void Start()
    {
        base.Start();
    }

    // Update is called once per frame
    new void Update()
    {
        base.Update();
    }

    protected override void Attack()
    {
        player.TakeTirement(damageAmount);
        // player and self paralize for 1s
    }
}