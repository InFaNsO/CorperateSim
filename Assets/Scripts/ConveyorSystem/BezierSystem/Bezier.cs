using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct OrientedPoint
{
    public Vector3 position;
    public Quaternion orientation;

    public OrientedPoint(Vector3 point)
    {
        this.position = point;
        this.orientation = Quaternion.identity;
    }
    public OrientedPoint(Vector3 pos, Quaternion q)
    {
        this.position = pos;
        this.orientation = q;
    }
    public OrientedPoint(Vector3 pos, Vector3 forward)
    {
        this.position = pos;
        this.orientation = Quaternion.LookRotation(forward);
    }

    public Vector3 LocalToWorldSpace(Vector3 posLocal)
    {
        return position + (orientation * posLocal);
    }
    public Vector3 LocalToWorldSpace(Vector2 posLocal)
    {
        var p = new Vector3(posLocal.x, posLocal.y, 0.0f);
        return position + (orientation * p);
    }
    public Vector3 LocalToWorldRotation(Vector2 posLocal)
    {
        var p = new Vector3(posLocal.x, posLocal.y, 0.0f);
        return orientation * p;
    }
}

public static class Bezier
{
    public static Vector3 EvaluateQuadratic(Vector3 a, Vector3 b, Vector3 c, float t)
    {
        Vector3 p0 = Vector3.Lerp(a, b, t);
        Vector3 p1 = Vector3.Lerp(b, c, t);
        return Vector3.Lerp(p0, p1, t);
    }
    public static Vector3 EvaluateCubic(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 p0 = EvaluateQuadratic(a, b, c, t);
        Vector3 p1 = EvaluateQuadratic(b, c, d, t);
        return Vector3.Lerp(p0, p1, t);
    }

    public static OrientedPoint EvaluateCubicOriented(Vector3 a, Vector3 b, Vector3 c, Vector3 d, float t)
    {
        Vector3 p0 = EvaluateQuadratic(a, b, c, t);
        Vector3 p1 = EvaluateQuadratic(b, c, d, t);
        return new OrientedPoint(Vector3.Lerp(p0, p1, t), (p1 - p0).normalized);

    }
}
