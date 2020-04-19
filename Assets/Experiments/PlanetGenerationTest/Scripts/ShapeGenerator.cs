using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class ShapeGenerator
    {
        ShapeSettings settings;
        INoiseFilter[] noiseFilters;
        public MinMax elevationMinMax;

        public void UpdateSettings(ShapeSettings shapeSettings)
        {
            settings = shapeSettings;
            noiseFilters = new INoiseFilter[settings.noiseLayers.Length];
            for (int i = 0; i < noiseFilters.Length; i++)
            {
                noiseFilters[i] = NoiseFilterFactory.CreateNoiseFilter(settings.noiseLayers[i].noiseSettings);
            }
            elevationMinMax = new MinMax();
        }

        public Vector3 CalculatePointOnPlanet(Vector3 point)
        {
            float firstLayerValue = 0.0f;
            float elevation = 0.0f;

            if (noiseFilters.Length > 0)
            {
                firstLayerValue = noiseFilters[0].Evaluate(point);
                if (settings.noiseLayers[0].enabled)
                    elevation = firstLayerValue;
            }

            for (int i = 0; i < noiseFilters.Length; i++)
            {
                if (settings.noiseLayers[i].enabled)
                {
                    float mask = (settings.noiseLayers[i].useFirstlayerAsMask) ? firstLayerValue : 1.0f;
                    elevation += noiseFilters[i].Evaluate(point) * mask;
                }
            }

            elevation = settings.planetRadius * (1 + elevation);
            elevationMinMax.AddValue(elevation);
            return point * elevation;
        }

        public void RegenerateNoise()
        {
            for (int i = 0; i < noiseFilters.Length; ++i)
            {
                noiseFilters[i].Regenerate();
            }
        }
    }
}