using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Noise
{
    public static float [,] GenerateNoiseMap(int mapWidth, int mapHeight, float scale, float octaves, float persistance, float lacunarity)
    {
        float[,] noiseMap = new float[mapWidth, mapHeight];

        if (scale <= 0)
            scale = 0.0001f;

        float max = float.MinValue;
        float min = float.MaxValue;

        for (int y = 0; y < mapHeight; y++)
        {
            for (int x = 0; x < mapWidth; x++)
            {
                float amplitude = 1.0f;
                float frequency = 1.0f;
                float noiseHeight = 1.0f;


                for (int i = 0; i < octaves; i++)
                {
                    float sampleX = x / scale * frequency;
                    float sampleY = y / scale * frequency;
                    float perlinVal = Mathf.PerlinNoise(sampleX, sampleY) * 2.0f - 1.0f;
                    noiseHeight += perlinVal * amplitude;

                    amplitude *= persistance;
                    frequency *= lacunarity;
                }

                if (noiseHeight > max)
                    max = noiseHeight;
                else if (noiseHeight < min)
                    min = noiseHeight;

                noiseMap[x, y] = noiseHeight;
            }
        }


        for (int y = 0; y < mapHeight; y++)
            for (int x = 0; x < mapWidth; x++)
                noiseMap[x, y] = Mathf.InverseLerp(min, max, noiseMap[x, y]);
        
        return noiseMap;
    }
}
