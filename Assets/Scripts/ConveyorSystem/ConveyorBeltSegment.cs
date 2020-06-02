using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;
using UnityEditor.PackageManager.Requests;

[RequireComponent(typeof(MeshFilter))]
[RequireComponent(typeof(MeshRenderer))]
[RequireComponent(typeof(MeshCollider))]
public class ConveyorBeltSegment : MonoBehaviour
{
    public Mesh2D shape;
    public Path path;

    MeshFilter myMesh;
    [SerializeField] MeshCollider myCollider;
    Mesh mesh;

    public OrientedPoint[] myPoints;

    [SerializeField] public ConveyorInputSlot startPos;
    [SerializeField] public ConveyorOutputSlot endPos;

    [SerializeField] bool remake;

    public float TimeToReachNextPoint = 1.0f;

    [SerializeField]float PointDistance = 1.5f;

    class ResourceData
    {
        public OrientedPoint point;
        public Resource currentResource = null;
    }
    List<ResourceData> MyResources = new List<ResourceData>();

    public bool HasResource()
    {
        return MyResources[0].currentResource;
    }
    public void AddResource(Resource r)
    {
        r.pointIndex = -1;
        if (MyResources[0].currentResource == null)
        {
            MyResources[0].currentResource = r;
            r.pointIndex = 0;
        }
    }

    private void Awake()
    {
        myMesh = GetComponent<MeshFilter>();
        myCollider = GetComponent<MeshCollider>();
        myMesh.sharedMesh = new Mesh();
        mesh = myMesh.sharedMesh;
        myCollider.sharedMesh = mesh;

        if (startPos && endPos)
        {
            path = new Path(startPos.transform, endPos.transform);
            //MakeMesh();
        }
    }
    public void ResetPath(ConveyorInputSlot start, ConveyorOutputSlot end)
    {
        startPos = start;
        endPos = end;
        path = new Path(endPos.transform, startPos.transform);
        MakeMesh();
        myPoints = path.CalculateEvenlySpaceOrientedPoints(PointDistance);

        MyResources.Clear();
        for(int i = 0; i < myPoints.Length - 1; ++i)
        {
            MyResources.Add(new ResourceData());
            MyResources[i].point = myPoints[i];
        }
        MyResources.RemoveAt(0);
        //MyResources.RemoveAt(0);
    }   

    private void OnValidate()
    {
        myMesh = GetComponent<MeshFilter>();
        myCollider = GetComponent<MeshCollider>();
        myMesh.sharedMesh = new Mesh();
        mesh = myMesh.sharedMesh;
        myCollider.sharedMesh = mesh;
        if (startPos && endPos)
        {
            path = new Path(startPos.transform, endPos.transform);
            MakeMesh();
        }
    }

    public void MakeMesh()
    {
        myPoints = path.CalculateEvenlySpaceOrientedPoints(0.4f);
        mesh.Clear();
        Mesh nMesh = new Mesh();
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

        nMesh.vertices = vertices.ToArray();
        nMesh.normals = normals.ToArray();
        nMesh.uv = uv.ToArray();
        nMesh.SetTriangles(indices.ToArray(), 0);

        myMesh.sharedMesh = nMesh;
        //myCollider.sharedMesh = nMesh;
        mesh = nMesh;

        //myCollider.sharedMesh = mesh;
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

        Gizmos.color = Color.green;
        for(int i = 0; i < myPoints.Length; ++i)
        {
            Gizmos.DrawSphere(myPoints[i].position, 0.1f);
        }

        for(int i = 0; i < MyResources.Count; ++i)
        {
            Gizmos.color = i % 2 == 0 ? Color.red : Color.blue;
            float rad = PointDistance * 0.5f;// i % 2 == 0 ? 0.5f : 1f;
            if (i % 2 == 0)
                Gizmos.DrawSphere(MyResources[i].point.position + Vector3.up, rad);
            else
                Gizmos.DrawWireSphere(MyResources[i].point.position + Vector3.up, rad);
        }
    }

    private void Update()
    {
        for(int i = 1; i < MyResources.Count; ++i)
        {
            if(MyResources[i].currentResource == null)
            {
                if(MyResources[i-1].currentResource)
                {
                    //move this to current
                    if (MyResources[i - 1].currentResource.t == 0f)
                        MyResources[i - 1].currentResource.startTime = Time.time - Time.deltaTime;

                    MoveCurrentResource(i - 1);
                }
            }
        }
    }

    public void RemoveResourceAtIndex(int i)
    {
        MyResources[i].currentResource.t = 1f;
        MyResources[i].currentResource = null;
    }

    void SetResource(int i)
    {
        var r = MyResources[i].currentResource;
        r.t = 0f;
        r.pointIndex = i;
        r.startTime = Time.time;
    }
    void MoveCurrentResource(int i)
    {
        var r = MyResources[i].currentResource;
        var opCurr = MyResources[i].point;
        var opNext = MyResources[i + 1].point;
        r.t = (Time.time - r.startTime) / (TimeToReachNextPoint);

        var p = Vector3.Lerp(opCurr.position, opNext.position, r.t);
        p.y += r.item.HeightOffset;
         r.transform.position = p;
         r.transform.rotation = Quaternion.Slerp(opCurr.orientation, opNext.orientation, r.t);

        //r.myRB.MovePosition(p);
        //r.myRB.MoveRotation(Quaternion.Slerp(opCurr.orientation, opNext.orientation, r.t));
        if (r.t >= 1.0f)
        {
            r.t = 0f;
            r.pointIndex = i + 1;
            MyResources[i + 1].currentResource = r;
            MyResources[i].currentResource = null;
        }
    }

    private void OnDestroy()
    {
        Debug.Log("Something destroyed the Belt");
//        startPos.belt = null;
//        endPos.belt = null;
        
        for(int i = 0; i < MyResources.Count; ++i)
        {
            if (MyResources[i].currentResource)
                Destroy(MyResources[i].currentResource.gameObject);
        }
    }
}



