using System.Collections;
using System.Collections.Generic;
using UnityEngine;


[CreateAssetMenu]
public class Mesh2D : ScriptableObject
{
    [System.Serializable]
    public class Vertex
    {
        public Vector2 point;
        public Vector2 normal;
        public float u; // just the u component of uv v will be calculated
    }

    public Vertex[] vertices;
    public Vector2Int[] lineSegements;
}
