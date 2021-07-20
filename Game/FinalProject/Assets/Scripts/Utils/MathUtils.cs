using System.Collections.Generic;
using UnityEngine;
public static class MathUtils
{
    public static float GetAbsXDistance(Vector3 from, Vector3 to)
    {
        return Mathf.Abs(from.x - to.x);
    }

    public static float GetyAbsYDistance(Vector3 from, Vector3 to)
    {
        return Mathf.Abs(from.y - to.y);
    }
    
    
    /// <summary>
    /// Gets the horizantal vector between two vectors. The direction starts from starting with a <paramref name="from"/> origin vector
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns><paramref name="from"/> + <see langword="Vector3.right"/> or <see langword="Vector3.left"/></returns>
    public static Vector3 GetXDirection(Vector3 from, Vector3 to)
    {
        return from + (to.x > from.x ? Vector3.right : Vector3.left);
    }

    /// <summary>
    /// Gets the vertical vector between two vectors. The direction starts from starting with a <paramref name="from"/> origin vector
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns><paramref name="from"/> + <see langword="Vector3.up"/> or <see langword="Vector3.down"/></returns>
    public static Vector3 GetYDirection(Vector3 from, Vector3 to)
    {
        return from + (to.y > from.y ? Vector3.up : Vector3.down);
    }

    public static Vector3 GetVectorFromAngle(float angle)
    {
        float angleRad = angle * (Mathf.PI/180f);
        return new Vector3(Mathf.Cos(angleRad), Mathf.Sin(angleRad));
    }

    public static float GetAngleFromVectorFloat(Vector3 dir)
    {
        dir = dir.normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        
        return n;
    }
    
    /// <summary>
    /// Gets angle in degrees (0° to 360°) between two vectors
    /// </summary>
    /// <param name="from"></param>
    /// <param name="to"></param>
    /// <returns></returns>
    public static float GetAngleBetween(Vector3 from, Vector3 to)
    {
        Vector3 dir = (to - from).normalized;
        float n = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        if (n < 0) n += 360;
        
        return n;
    }

    public static Vector3 FindCenterOfTransforms(List<Transform> transforms)
    {
        var bound = new Bounds(transforms[0].position, Vector3.zero);
        for(int i = 1; i < transforms.Count; i++)
        {
            bound.Encapsulate(transforms[i].position);
        }
        return bound.center;
    }

    public static Vector3 FindCenterOfTransforms(List<GameObject> gameObjects)
    {
        var bound = new Bounds(gameObjects[0].transform.position, Vector3.zero);
        for(int i = 1; i < gameObjects.Count; i++)
        {
            bound.Encapsulate(gameObjects[i].transform.position);
        }
        return bound.center;
    }

    public static int EditBitInBitmask(int bitmask, int pos, bool value)
    {
        if (value)
        {
            return bitmask |= (1 << pos);
        }
        else
        {
            return bitmask & ~(1 << pos);
        }
    }
}