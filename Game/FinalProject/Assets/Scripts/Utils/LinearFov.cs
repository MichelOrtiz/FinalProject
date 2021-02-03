using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class LinearFov
{
    private const string LEFT = "left";
    private const string RIGHT = "right";
    // Start is called before the first frame update
    public static bool CanSeePlayer(float distance, string facingDirection, Transform castPos)
    {
        Vector2 endPos;
        //endPos = castPos.position + Vector3.left * distance;
        if (facingDirection == LEFT)
        {
            endPos = castPos.position + Vector3.left * distance;
        }
        else
        {
            endPos = castPos.position + Vector3.right * distance;
        }

        RaycastHit2D hit = Physics2D.Linecast(castPos.position, endPos, 1 << LayerMask.NameToLayer("Action"));
        
        if (hit.collider == null)
        {
            return false;
        }
        Debug.DrawLine(castPos.position, endPos, Color.blue);
        return hit.collider.gameObject.CompareTag("Player");
    }
}