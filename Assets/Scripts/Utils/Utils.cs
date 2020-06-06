using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static float ClampAngle(float angle, float from, float to)
    {
        // accepts e.g. -80, 80
        if (angle < 0f) angle = 360 + angle;
        if (angle > 180f) return Mathf.Max(angle, 360+from);
        return Mathf.Min(angle, to);
    }
    
    public static float WrapAngle (float angle)
    {
        if (angle > 180) angle = angle - 360;
        
        return angle;
    }

}
