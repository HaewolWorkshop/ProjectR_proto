using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class VectorExtension
{
    public static Vector2 xy(this Vector3 v)
    {
        return new Vector2(v.x, v.y);
    }


    public static Vector3 RotateToTransformSpace(this Vector3 vector, Transform target)
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
    
    /// <summary>
    /// Y축 기준으로 벡터를 반시계방향 회전합니다. 
    /// </summary>
    /// <param name="v">대상 벡터입니다.</param>
    /// <param name="angleInRadian">회전할 라디안 단위 각도입니다. 반시계 방향으로 회전합니다.</param>
    /// <returns>자신의 벡터를 반환합니다.</returns>
    public static Vector3 RotateAroundY(this ref Vector3 v, float angleInRadian) {
        float cos = Mathf.Cos(angleInRadian);
        float sin = Mathf.Sin(angleInRadian);
        float x = v.x * cos - v.z * sin;
        float z = v.z * cos + v.x * sin;
        v.x = x;
        v.z = z;
        return v;
    }

    /// <summary>
    /// Y축 기준으로 벡터를 반시계방향 회전한 벡터를 복사해서 반환합니다.
    /// </summary>
    /// <param name="v">대상 벡터입니다.</param>
    /// <param name="angleInRadian">회전할 라디안 단위 각도입니다. 반시계 방향으로 회전합니다.</param>
    /// <returns>복사본 벡터를 반환합니다.</returns>
    public static Vector3 RotatedAroundY(this in Vector3 v, float angleInRadian) {
        Vector3 newVector = new Vector3(v.x, v.y, v.z);
        newVector.RotateAroundY(angleInRadian);
        return newVector;
    }
}
