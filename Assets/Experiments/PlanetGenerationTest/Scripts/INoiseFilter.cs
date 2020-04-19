using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace PlanetGeneration
{
    public class INoiseFilter
    {
        public Noise noise = new Noise();
        public virtual float Evaluate(Vector3 point)
        {
            return 0.0f;
        }

        public void Regenerate()
        {
            noise = new Noise(Random.Range(-20, 20));
        }
        public void Regenerate(int seed)
        {
            noise = new Noise(seed);
        }
    }
}