using UnityEngine;
public class MirrorReflex : PlayerClone 
{
    [SerializeField] Transform mirrorOrigin;

    protected override void CloneMovement()
    {
        // Inverts the x scale, so the movement is inverted
        if (transform.lossyScale.x > 0)
        {
            transform.localScale = new Vector3(-1, 1, 1);
        }
        // Mimics symmetrically the player movements by the x axis, based on the mirror position
        transform.position = new Vector3(mirrorOrigin.position.x, 0, 0) + new Vector3(mirrorOrigin.position.x - currentCloner.position.x, currentCloner.position.y, currentCloner.position.z);
        transform.rotation = currentCloner.rotation;
    }
}