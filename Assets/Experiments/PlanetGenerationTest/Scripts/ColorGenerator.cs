﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class ColorGenerator
    {
        ColorSettings settings;
        Texture2D texture;
        const int textureResolution = 50;
        INoiseFilter biomeNoiseFilter;

        public void UpdateSettings(ColorSettings settings)
        {
            this.settings = settings;
            if (texture == null || texture.height != settings.biomeColorSettings.biomes.Length)
                texture = new Texture2D(textureResolution, settings.biomeColorSettings.biomes.Length);

            biomeNoiseFilter = NoiseFilterFactory.CreateNoiseFilter(settings.biomeColorSettings.noise);
        }

        public void UpdateElevation(MinMax elevationLevel)
        {
            settings.planetMaterial.SetVector("_elevationMinMax", new Vector4(elevationLevel.Min, elevationLevel.Max));
        }

        public float BiomePercentFromPoint(Vector3 point)
        {
            float heightPercent = (point.y + 1) * 0.5f;
            heightPercent += (biomeNoiseFilter.Evaluate(point) - settings.biomeColorSettings.noiseOffset) * settings.biomeColorSettings.noiseStrength;

            float biomeIndex = 0;
            int numBiomes = settings.biomeColorSettings.biomes.Length;
            float blendRange = settings.biomeColorSettings.blendAmmount * 0.5f + 0.0001f;

            for (int i = 0; i < numBiomes; i++)
            {
                float dst = heightPercent - settings.biomeColorSettings.biomes[i].startHeight;
                float weight = Mathf.InverseLerp(-blendRange, blendRange, dst);
                biomeIndex *= (1 - weight);
                biomeIndex += i + weight;
            }

            return biomeIndex / Mathf.Max(1, numBiomes - 1);
        }

        public void UpdateColors()
        {
            Color[] colors = new Color[texture.width * texture.height];
            int colorIndex = 0;
            foreach (var biome in settings.biomeColorSettings.biomes)
            {
                for (int i = 0; i < textureResolution; i++)
                {
                    Color gradientCol = biome.gradient.Evaluate(i / (textureResolution - 1.0f));
                    Color tintCol = biome.tint;
                    colors[colorIndex] = gradientCol * (1 - biome.tintPercent) + tintCol * biome.tintPercent;
                    colorIndex++;
                }
            }

            texture.SetPixels(colors);
            texture.Apply();
            settings.planetMaterial.SetTexture("_texture", texture);
        }
    }
}