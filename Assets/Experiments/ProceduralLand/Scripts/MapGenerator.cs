using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public int MapWidth;
    public int MapHeight;
    public float noiseScale;

    private void OnValidate()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        //float[,] noiseMap = Noise.GenerateNoiseMap(MapWidth, MapHeight, noiseScale);

        MapDisplay display = GetComponent<MapDisplay>();

        //display.DrawNoiseMap(ref noiseMap);
    }
}
