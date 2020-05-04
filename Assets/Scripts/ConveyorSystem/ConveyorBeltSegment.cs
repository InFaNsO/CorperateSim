using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ConveyorBeltSegment : MonoBehaviour
{
    public Mesh2D shape;
    Path path;

    MeshFilter myMesh;
    MeshCollider myCollider;
    Mesh mesh;

    public OrientedPoint[] myPoints;

    [SerializeField] public Transform startPos;
    [SerializeField] public Transform endPos;

    [SerializeField] bool remake;

    private void Awake()
    {
        myMesh = GetComponent<MeshFilter>();
        myCollider = GetComponent<MeshCollider>();
        myMesh.sharedMesh = new Mesh();
        mesh = myMesh.sharedMesh;

        if (startPos && endPos)
        {
            path = new Path(startPos, endPos);
            MakeMesh();
        }
    }
    public void ResetPath(Transform start, Transform end)
    {
        startPos = start;
        endPos = end;
        path = new Path(startPos, endPos);
        MakeMesh();
    }

    private void OnValidate()
    {
        myMesh = GetComponent<MeshFilter>();
        myCollider = GetComponent<MeshCollider>();
        myMesh.sharedMesh = new Mesh();
        mesh = myMesh.sharedMesh;

        if (startPos && endPos)
        {
            path = new Path(startPos, endPos);
            MakeMesh();
        }
    }

    public void MakeMesh()
    {
        myPoints = path.CalculateEvenlySpaceOrientedPoints(0.4f);
        mesh.Clear();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals= new List<Vector3>();
        List<int> indices= new List<int>();

        List<Vector2> uv = new List<Vector2>();

        for(int i = 2; i < myPoints.Length; ++i)
        {
            for(int j = 0; j < shape.lineSegements.Length; ++j)
            {
                //make a quad
                int startInd = vertices.Count;
                vertices.Add(myPoints[i - 1].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].x].point));
                vertices.Add(myPoints[i - 1].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].y].point));
                vertices.Add(myPoints[i    ].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].x].point));
                vertices.Add(myPoints[i    ].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].y].point));

                normals.Add(myPoints[i - 1].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].x].normal).normalized);
                normals.Add(myPoints[i - 1].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].y].normal).normalized);
                normals.Add(myPoints[i    ].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].x].normal).normalized);
                normals.Add(myPoints[i    ].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].y].normal).normalized);

                uv.Add(new Vector2(shape.vertices[shape.lineSegements[j].x].u, 0.0f));
                uv.Add(new Vector2(shape.vertices[shape.lineSegements[j].y].u, 0.0f));

                uv.Add(new Vector2(shape.vertices[shape.lineSegements[j].x].u, 1.0f));
                uv.Add(new Vector2(shape.vertices[shape.lineSegements[j].y].u, 1.0f));


                indices.Add(startInd);
                indices.Add(startInd + 2);
                indices.Add(startInd + 1);
                
                indices.Add(startInd + 1);
                indices.Add(startInd + 2);
                indices.Add(startInd + 3);
            }
        }

        mesh.vertices = vertices.ToArray();
        mesh.normals = normals.ToArray();
        mesh.uv = uv.ToArray();
        mesh.SetTriangles(indices.ToArray(), 0);

        myCollider.sharedMesh.Clear();
        myCollider.sharedMesh = mesh;
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < path.NumSegments; ++i)
        {
            var points = path.GetPointsInSegment(i);

            Handles.color = Color.black;
            Handles.DrawLine(points[1], points[0]);
            Handles.DrawLine(points[2], points[3]);

            Handles.DrawBezier(points[0], points[3], points[1], points[2], Color.green, null, 2);
        }

        Handles.color = Color.red;
        for (int i = 0; i < path.NumPoints; ++i)
        {
            var newPos = Handles.FreeMoveHandle(path[i], Quaternion.identity, 0.1f, Vector3.zero, Handles.SphereHandleCap);
            if (newPos != path[i])
            {
                //Undo.RecordObject(creator, "Move Point");
                path.MovePoint(i, newPos);
            }
        }
    }
}
