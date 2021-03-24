using UnityEngine;
public class Dragon : NormalType
{
    
    protected new void Start()
    {
        base.Start();
    }

    protected new void Update()
    {
        base.Update();
    }

    protected new void FixedUpdate()
    {
        base.FixedUpdate();
    }

    public void EnhanceValues(float multiplier)
    {
        normalSpeed *= multiplier;
        chaseSpeed *= multiplier;
        damageAmount *= multiplier;
    }

    public void NerfValues(float divider)
    {
        normalSpeed /= divider;
        chaseSpeed /= divider;
        damageAmount /= divider;
    }
}
