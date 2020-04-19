using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class RigidNoiseFilter : INoiseFilter
    {
        NoiseSettings.RidgidNoiseSettings settings;

        public RigidNoiseFilter(NoiseSettings.RidgidNoiseSettings settings)
        {
            this.settings = settings;
        }

        public override float Evaluate(Vector3 point)
        {
            float noiseValue = 0.0f;
            float frequency = settings.baseRoughness;
            float amplitude = 1.0f;
            float weight = 1.0f;

            for (int i = 0; i < settings.numLayers; i++)
            {
                float v = 1 - Mathf.Abs(noise.Evaluate(point * frequency + settings.centre));
                v *= v;
                v *= weight;

                weight = Mathf.Clamp01(v * settings.weightMultiplier);

                noiseValue += v * amplitude;
                frequency *= settings.roughness;
                amplitude *= settings.persistence;
            }

            noiseValue = Mathf.Max(0.0f, noiseValue - settings.minValue);
            return noiseValue * settings.strength;
        }
    }
}