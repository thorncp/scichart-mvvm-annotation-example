
using System;
using System.Collections.Generic;

namespace AnnotationsMvvm.Data
{
    public class RandomWalkGenerator
    {
        private readonly Random _random;
        private double _last;
        private int _i;
        private const double Bias = 0.01;

        public RandomWalkGenerator()
        {
            _random = new Random();
        }

        public RandomWalkGenerator(int seed)
        {
            _random = new Random(seed);
        }

        public DoubleSeries GetRandomWalkSeries(int count)
        {
            var doubleSeries = new DoubleSeries(count);

            // Generate a slightly positive biased random walk
            // y[i] = y[i-1] + random, 
            // where random is in the range -0.5, +0.5
            for (int i = 0; i < count; i++)
            {
                double next = _last + (_random.NextDouble() - 0.5 + Bias);
                _last = next;

                doubleSeries.Add(new XyPoint() { X = _i++, Y = next });
            }

            return doubleSeries;
        }
    }
}