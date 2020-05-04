 using System.Collections;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Security.Cryptography;
using UnityEngine;

[System.Serializable]
public class Path
{
    [SerializeField, HideInInspector]
    List<Vector3> points;
    //[SerializeField, HideInInspector]
    //List<Transform> controlPoints = new List<Transform>();
    [SerializeField, HideInInspector]
    bool isClosed;
    [SerializeField, HideInInspector]
    bool isAutoSet;

    public Path(Vector3 center)
    {
        points = new List<Vector3>
        {
            center+Vector3.left ,
            center+(Vector3.left + Vector3.forward) * 0.5f,
            center + Vector3.right,
            center + (Vector3.right + Vector3.back) * 0.5f
        };
    }
    public Path(Transform start, Transform end)
    {
        float halfLen = (end.position - start.position).magnitude * 0.5f;
        points = new List<Vector3>
        {
            start.position,
            start.position + (start.right * halfLen),
            end.position + (end.right * halfLen),
            end.position
        };
        //AutoSetControlPoints = true;
    }

    public Vector3 this[int i] { get { return points[i]; } }
    public int NumPoints { get { return points.Count; } }
    public int NumSegments { get { return points.Count / 3; } }
    public bool IsClosed
    {
        get
        {
            return isClosed;
        }
        set
        {
            isClosed = value;

            if (isClosed)
            {
                points.Add(points[points.Count - 1] * 2f - points[points.Count - 2]);
                points.Add(points[0] * 2f - points[1]);

                if (isAutoSet)
                {
                    AutoSetAnchorControlls(0);
                    AutoSetAnchorControlls(points.Count - 3);
                }
            }
            else
            {
                points.RemoveRange(points.Count - 2, 2);
                if (isAutoSet)
                    AutoSetStartAndEnd();
            }
        }
    }
    public bool AutoSetControlPoints 
    { 
        get 
        { 
            return isAutoSet; 
        }
        set
        {
            if(isAutoSet != value)
            {
                isAutoSet = value;
                if (isAutoSet)
                    AutoSetAllControlPoints();
            }
        }
    }

    public void AddSegment(Vector3 anchorPos)
    {
        points.Add(points[points.Count - 1] * 2f - points[points.Count - 2]);
        points.Add((points[points.Count - 1] + anchorPos) * 0.5f);
        points.Add(anchorPos);

        if (isAutoSet)
            AutoSetAffectedCP(points.Count - 1);
    }

    public void SplitSegment(Vector3 pos, int segmentIndex)
    {
        points.InsertRange(segmentIndex * 3 + 2, new Vector3[] { Vector3.zero, pos, Vector3.zero });
        if (isAutoSet)
            AutoSetAffectedCP(segmentIndex * 3 + 3);
        else
            AutoSetAnchorControlls(segmentIndex * 3);
    }

    public Vector3[] GetPointsInSegment(int i)
    {
        return new Vector3[] { points[i * 3], points[i * 3 + 1], points[i * 3 + 2], points[LoopIndex(i * 3 + 3)] };
    }

    public void MovePoint(int i, Vector3 newPos)
    {
        Vector3 deltaMove = newPos - points[i];

        if (i % 3 == 0 || !isAutoSet)
        {
            points[i] = newPos;

            if (isAutoSet)
            {
                AutoSetAffectedCP(i);
                return;
            }

            if (i % 3 == 0)
            {
                //its an anchor point
                if (i + 1 < points.Count || isClosed)
                    points[LoopIndex(i + 1)] += deltaMove;
                if (i - 1 >= 0 || isClosed)
                    points[LoopIndex(i - 1)] += deltaMove;
            }
            else
            {
                //its a controll point
                bool nexPointIsAnchor = (i + 1) % 3 == 0;
                int correspondingControlIndex = nexPointIsAnchor ? i + 2 : i - 2;
                int anchorIndex = nexPointIsAnchor ? i + 1 : i - 1;

                if (correspondingControlIndex >= 0 && correspondingControlIndex < points.Count || isClosed)
                {
                    anchorIndex = LoopIndex(anchorIndex);
                    correspondingControlIndex = LoopIndex(correspondingControlIndex);
                    float distance = (points[anchorIndex] - points[correspondingControlIndex]).magnitude;
                    Vector3 dir = (points[anchorIndex] - newPos).normalized;
                    points[correspondingControlIndex] = points[anchorIndex] + dir * distance;
                }
            }
        }
    }

    public Vector3[] CalculateEvenlySpacePoints(float spacing, float resolution = 1f)
    {
        List<Vector3> spacedPoints = new List<Vector3>();
        spacedPoints.Add(points[0]);
        var previousPoint = points[0];
        float distanceFromPrevious = 0f;

        for(int segIndex = 0; segIndex < NumSegments; ++segIndex)
        {
            Vector3[] p = GetPointsInSegment(segIndex);
            float controlNetLength = Vector3.Distance(p[0], p[1]) + Vector3.Distance(p[1], p[2]) + Vector3.Distance(p[2], p[3]);
            float estimatedCurveLEngth = Vector3.Distance(p[0], p[3]) + (controlNetLength * 0.5f);
            int divisions = (int)estimatedCurveLEngth * (int)resolution * 10;

            float t = 0f;
            while (t <= 1f)
            {
                t += 1f / divisions;
                var poc = Bezier.EvaluateCubic(p[0], p[1], p[2], p[3], t);
                distanceFromPrevious += Vector3.Distance(previousPoint, poc);

                while (distanceFromPrevious >= spacing)
                {
                    float overShoot = distanceFromPrevious - spacing;
                    Vector3 newPoint = poc + (previousPoint - poc).normalized * overShoot;
                    spacedPoints.Add(newPoint);
                    distanceFromPrevious = overShoot;
                    previousPoint = newPoint;
                }
                previousPoint = poc;
            }

        }
        return spacedPoints.ToArray();
    }

    public OrientedPoint[] CalculateEvenlySpaceOrientedPoints(float spacing, float resolution = 1.0f)
    {
        List<OrientedPoint> spacedPoints = new List<OrientedPoint>();
        spacedPoints.Add(new OrientedPoint(points[0]));
        var previousPoint = new OrientedPoint(points[0]);
        float distanceFromPrevious = 0f;

        for (int segIndex = 0; segIndex < NumSegments; ++segIndex)
        {
            Vector3[] p = GetPointsInSegment(segIndex);
            float controlNetLength = Vector3.Distance(p[0], p[1]) + Vector3.Distance(p[1], p[2]) + Vector3.Distance(p[2], p[3]);
            float estimatedCurveLEngth = Vector3.Distance(p[0], p[3]) + (controlNetLength * 0.5f);
            int divisions = (int)estimatedCurveLEngth * (int)resolution * 10;

            var pointStart = Bezier.EvaluateCubicOriented(p[0], p[1], p[2], p[3], 0.0f);
            spacedPoints.Add(pointStart);
            previousPoint = pointStart;

            float t = 0f;
            while (t < 1f)
            {
                t += 1f / divisions;
                var poc = Bezier.EvaluateCubicOriented(p[0], p[1], p[2], p[3], t);
                distanceFromPrevious += Vector3.Distance(previousPoint.position, poc.position);

                while (distanceFromPrevious >= spacing)
                {
                    float overShoot = distanceFromPrevious - spacing;
                    Vector3 newPoint = poc.position + (previousPoint.position - poc.position).normalized * overShoot;
                    float deltaLen = (newPoint - previousPoint.position).magnitude;
                    float percent = overShoot / deltaLen;
                    Quaternion newOritentation = Quaternion.Slerp(previousPoint.orientation, poc.orientation, percent);
                    spacedPoints.Add(new OrientedPoint(newPoint, newOritentation));
                    distanceFromPrevious = overShoot;
                    previousPoint = new OrientedPoint(newPoint);
                }
                previousPoint = poc;
            }

            var pointFinal = Bezier.EvaluateCubicOriented(p[0], p[1], p[2], p[3], 1.0f);
            spacedPoints.Add(pointFinal);
            previousPoint = pointFinal;

        }
        return spacedPoints.ToArray();

    }

    void AutoSetAffectedCP(int anchorIndex)
    {
        for (int i = anchorIndex - 3; i <= anchorIndex + 3; i += 3)
            if (i >= 0 && i < points.Count || isClosed)
                AutoSetAnchorControlls(LoopIndex(i));
        AutoSetStartAndEnd();
    }
    void AutoSetAllControlPoints()
    {
        for(int i = 0; i < points.Count; i += 3)
        {
            AutoSetAnchorControlls(i);
        }

        AutoSetStartAndEnd();
    }

    void AutoSetAnchorControlls(int anchorIndex)
    {
        var anchorPos = points[anchorIndex];
        var dir = Vector3.zero;
        float[] neghbouringDistance = new float[2];

        if(anchorIndex - 3 >= 0 || isClosed)
        {
            var offset = points[LoopIndex(anchorIndex - 3)] - anchorPos;
            dir += offset.normalized;
            neghbouringDistance[0] = offset.magnitude;
        }
        if (anchorIndex + 3 >= 0 || isClosed)
        {
            var offset = points[LoopIndex(anchorIndex + 3)] - anchorPos;
            dir -= offset.normalized;
            neghbouringDistance[1] = -offset.magnitude;
        }
        dir.Normalize();

        for(int i = 0; i < 2; ++i)
        {
            int controllIndex = anchorIndex + i * 2 - 1;
            if(controllIndex >= 0 && controllIndex < points.Count || isClosed)
            {
                points[LoopIndex(controllIndex)] = anchorPos + dir * neghbouringDistance[i] * 0.5f;
            }
        }
    }
    void AutoSetStartAndEnd()
    {
        if (isClosed)
            return;

        points[1] = (points[0] + points[2]) * 0.5f;
        points[points.Count - 2] = (points[points.Count - 1] + points[points.Count - 3]) * 0.5f;
    }

    public void DeleteSegment(int anchorIndex)
    {
        Debug.Assert(anchorIndex % 3 == 0, "Wrong index is being deleted");

        if (NumSegments > 2 || !isClosed && NumSegments > 1)
        {
            if (anchorIndex == 0)
            {
                if (isClosed)
                    points[points.Count - 1] = points[2];
                points.RemoveRange(0, 3);
            }
            else if (anchorIndex == points.Count - 1 && !isClosed)
                points.RemoveRange(anchorIndex - 2, 3);
            else
                points.RemoveRange(anchorIndex - 1, 3);
        }
    }

    int LoopIndex(int i)
    {
        return (i + points.Count) % points.Count;
    }
}
