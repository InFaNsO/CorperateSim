﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
public class Wire : MonoBehaviour
{
    public ElectricNode nodeStart = null;
    public ElectricNode nodeEnd = null;

    Path path;

    Mesh2D shape;
    int NumShapeVertices = 10;
    [SerializeField] float radius = 0.5f;

    MeshFilter myMesh;
    Mesh mesh;

    private void Awake()
    {
        myMesh = GetComponent<MeshFilter>();
        myMesh.sharedMesh = new Mesh();
        mesh = myMesh.sharedMesh;
        MakeShape();

    }

    void MakeShape()
    {
        float angle = 0.0f;
        float deltaAngle = 360.0f / NumShapeVertices;
        float u = 0.0f;
        float deltaU = 1f / NumShapeVertices;

        shape.vertices = new Mesh2D.Vertex[NumShapeVertices];
        shape.lineSegements = new Vector2Int[NumShapeVertices];

        Vector2 pStart = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
        angle += deltaAngle;
        shape.vertices[0].point = pStart;
        shape.vertices[0].normal = pStart.normalized;
        shape.vertices[0].u = u;
        u += deltaU;

        for (int i = 1; i < NumShapeVertices; ++i)
        {
            Vector2 p1 = new Vector2(Mathf.Cos(angle) * radius, Mathf.Sin(angle) * radius);
            angle += deltaAngle;
            shape.vertices[i].point = p1;
            shape.vertices[i].normal = p1.normalized;
            shape.vertices[i].u = u;
            u += deltaU;

            shape.lineSegements[i] = new Vector2Int(i - 1, i);
        }
    }

    public void ResetPath(ElectricNode start, ElectricNode end)
    {
        nodeStart = start;
        nodeEnd = end;
        path = new Path(nodeStart.transform, nodeEnd.transform);

        //reset controll points
        float halfDist = (nodeStart.transform.position + nodeEnd.transform.position).magnitude * 0.5f;
        var cont1 = (nodeEnd.transform.position - nodeStart.transform.position).normalized * halfDist;
        cont1.y -= nodeStart.transform.position.y * 0.5f;
        path.MovePoint(1, cont1);
        var cont2 = (-nodeEnd.transform.position + nodeStart.transform.position).normalized * halfDist;
        cont2.y -= nodeEnd.transform.position.y * 0.5f;
        path.MovePoint(2, cont2);

        MakeMesh();
    }
    public void MakeMesh()
    {
        var myPoints = path.CalculateEvenlySpaceOrientedPoints(0.4f);
        mesh.Clear();
        List<Vector3> vertices = new List<Vector3>();
        List<Vector3> normals = new List<Vector3>();
        List<int> indices = new List<int>();

        List<Vector2> uv = new List<Vector2>();

        for (int i = 2; i < myPoints.Length; ++i)
        {
            for (int j = 0; j < shape.lineSegements.Length; ++j)
            {
                //make a quad
                int startInd = vertices.Count;
                vertices.Add(myPoints[i - 1].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].x].point));
                vertices.Add(myPoints[i - 1].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].y].point));
                vertices.Add(myPoints[i].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].x].point));
                vertices.Add(myPoints[i].LocalToWorldSpace(shape.vertices[shape.lineSegements[j].y].point));

                normals.Add(myPoints[i - 1].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].x].normal).normalized);
                normals.Add(myPoints[i - 1].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].y].normal).normalized);
                normals.Add(myPoints[i].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].x].normal).normalized);
                normals.Add(myPoints[i].LocalToWorldRotation(shape.vertices[shape.lineSegements[j].y].normal).normalized);

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
    }
}
