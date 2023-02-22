using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 xy(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }


    public static Vector3 ConvertToTransformSpace(this Vector3 vector, Transform target)
    {
        var forward = target.forward;
        var right = target.right;
        forward.y = 0;
        right.y = 0;

        forward.Normalize();
        right.Normalize();

        right *= vector.x;
        forward *= vector.z;

        return right + forward;
    }
}
