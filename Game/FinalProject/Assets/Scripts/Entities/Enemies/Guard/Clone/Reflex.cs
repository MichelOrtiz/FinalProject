using UnityEngine;
public class Reflex : PlayerClone
{
    protected override void CloneMovement()
    {
        // Reverts the x scale, so the movement is normal
        if (transform.lossyScale.x < 0)
        {
            transform.localScale = new Vector3(1, 1, 1);
        }
        // Copies the cloner position and rotation
        transform.position = currentCloner.position;
        transform.rotation = currentCloner.rotation;
    }
}