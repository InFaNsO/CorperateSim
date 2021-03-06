﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class Planet : MonoBehaviour
    {
        [Range(2, 256)]
        public int resolution = 10;
        public bool autoUpdate = true;
        public enum FaceRenderMask { All, Top, Buttom, Left, Right, Front, Back };
        public FaceRenderMask faceRenderMask;

        public ShapeSettings shapeSettings;
        public ColorSettings colorSettings;

        [HideInInspector] public bool shapeSettingsFoldout = false;
        [HideInInspector] public bool colorSettingsFoldout = false;

        public ShapeGenerator shapeGenerator = new ShapeGenerator();
        public ColorGenerator colorGenerator = new ColorGenerator();

        [HideInInspector] public MeshFilter[] meshFilters;
        TerrainFace[] terrainFaces;

        void Initialise()
        {
            shapeGenerator.UpdateSettings(shapeSettings);
            colorGenerator.UpdateSettings(colorSettings);

            if (meshFilters == null || meshFilters.Length == 0)
                meshFilters = new MeshFilter[6];
            terrainFaces = new TerrainFace[6];

            Vector3[] Directions = { Vector3.up, Vector3.down, Vector3.left, Vector3.right, Vector3.forward, Vector3.back };

            for (int i = 0; i < 6; i++)
            {
                if (meshFilters[i] == null)
                {
                    GameObject meshObj = new GameObject("mesh");
                    meshObj.transform.parent = transform;

                    meshObj.AddComponent<MeshRenderer>();
                    meshFilters[i] = meshObj.AddComponent<MeshFilter>();
                    meshFilters[i].sharedMesh = new Mesh();
                }

                meshFilters[i].GetComponent<MeshRenderer>().sharedMaterial = colorSettings.planetMaterial;

                terrainFaces[i] = new TerrainFace(shapeGenerator, meshFilters[i].sharedMesh, resolution, Directions[i]);
                bool renderFace = faceRenderMask == FaceRenderMask.All || (int)faceRenderMask - 1 == i;
                meshFilters[i].gameObject.SetActive(renderFace);
            }
        }

        public void GeneratePlanet()
        {
            Initialise();
            GenerateMesh();
            GenerateColors();
        }

        public void OnShapeSettingsUpdated()
        {
            if (autoUpdate)
            {
                Initialise();
                GenerateMesh();
            }
        }

        public void OnColorSettingsUpdated()
        {
            if (autoUpdate)
            {
                Initialise();
                GenerateColors();
            }
        }

        void GenerateMesh()
        {
            for (int i = 0; i < 6; i++)
                if (meshFilters[i].gameObject.activeSelf)
                    terrainFaces[i].ConstructMesh();

            colorGenerator.UpdateElevation(shapeGenerator.elevationMinMax);
        }

        void GenerateColors()
        {
            colorGenerator.UpdateColors();
            for (int i = 0; i < 6; i++)
                if (meshFilters[i].gameObject.activeSelf)
                    terrainFaces[i].UpdateUVs(colorGenerator);
        }
    }
}