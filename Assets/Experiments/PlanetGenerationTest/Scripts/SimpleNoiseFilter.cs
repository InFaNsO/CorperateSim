using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class SimpleNoiseFilter : INoiseFilter
    {
        NoiseSettings.SimpleNoiseSettings settings;
        public SimpleNoiseFilter(NoiseSettings.SimpleNoiseSettings settings)
        {
            this.settings = settings;
        }

        public override float Evaluate(Vector3 point)
        {
            float noiseValue = 0.0f;
            float frequency = settings.baseRoughness;
            float amplitude = 1.0f;

            for (int i = 0; i < settings.numLayers; i++)
            {
                float v = noise.Evaluate(point * frequency + settings.centre);
                noiseValue += (v + 1) * 0.5f * amplitude;
                frequency *= settings.roughness;
                amplitude *= settings.persistence;
            }

            noiseValue = Mathf.Max(0.0f, noiseValue - settings.minValue);
            return noiseValue * settings.strength;
        }
    }
}