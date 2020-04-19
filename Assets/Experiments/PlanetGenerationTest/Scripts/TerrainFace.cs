using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class TerrainFace
    {
        ShapeGenerator shapeGenerator;
        Mesh mesh;
        int resolution;
        Vector3 localUp;
        Vector3 axisA;
        Vector3 axisB;

        public TerrainFace(ShapeGenerator shapeGenerator, Mesh mesh, int resolution, Vector3 up)
        {
            this.shapeGenerator = shapeGenerator;
            this.mesh = mesh;
            this.resolution = resolution;

            localUp = up;

            axisA = new Vector3(localUp.y, localUp.z, localUp.x);
            axisB = Vector3.Cross(up, axisA);
        }

        public void ConstructMesh()
        {
            Vector3[] vertices = new Vector3[resolution * resolution];
            int[] triangles = new int[(resolution - 1) * (resolution - 1) * 6];
            int triangleIndex = 0;
            Vector2[] uv = mesh.uv;

            for (int y = 0; y < resolution; ++y)
            {
                for (int x = 0; x < resolution; x++)
                {
                    int i = x + y * resolution;
                    Debug.Assert(resolution > 1);
                    Vector2 percent = new Vector2(x, y) / (resolution - 1);
                    Vector3 point = localUp + (percent.x - 0.5f) * 2.0f * axisA + (percent.y - 0.5f) * 2.0f * axisB;
                    point.Normalize();
                    vertices[i] = shapeGenerator.CalculatePointOnPlanet(point);

                    if (x != resolution - 1 && y != resolution - 1)
                    {
                        triangles[triangleIndex] = i;
                        triangles[triangleIndex + 1] = i + resolution + 1;
                        triangles[triangleIndex + 2] = i + resolution;

                        triangles[triangleIndex + 3] = i;
                        triangles[triangleIndex + 4] = i + 1;
                        triangles[triangleIndex + 5] = i + resolution + 1;
                        triangleIndex += 6;
                    }
                }
            }

            mesh.Clear();
            mesh.vertices = vertices;
            mesh.triangles = triangles;
            mesh.RecalculateNormals();
            mesh.uv = uv;
        }

        public void UpdateUVs(ColorGenerator colorGenerator)
        {
            Vector2[] uv = new Vector2[resolution * resolution];

            for (int y = 0; y < resolution; y++)
            {
                for (int x = 0; x < resolution; x++)
                {
                    int i = x + y * resolution;
                    Debug.Assert(resolution > 1);
                    Vector2 percent = new Vector2(x, y) / (resolution - 1);
                    Vector3 point = localUp + (percent.x - 0.5f) * 2.0f * axisA + (percent.y - 0.5f) * 2.0f * axisB;
                    point.Normalize();

                    uv[i] = new Vector2(colorGenerator.BiomePercentFromPoint(point), 0.0f);
                }
            }
        }
    }
}