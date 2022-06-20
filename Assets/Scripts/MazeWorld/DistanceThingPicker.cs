using System.Collections.Generic;
using Algorithms;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace MazeWorld
{
    // this class gets and returns objects based on its distance ratio value to goal cell
    public class DistanceThingPicker<T> where T: IHasDistanceValue
    {
        private readonly DistanceDistribution _distanceDistribution;
        private readonly List<T> _things;

        public DistanceThingPicker(DistanceDistribution distanceDistribution, List<T> things)
        {
            _distanceDistribution = distanceDistribution;
            _things = things;
        }

        public Result<T> GetThingBasedOnDistance(float distance)
        {
            var creatures = GetThingsBasedOnDistance(distance);
            if (creatures == null || creatures.Count == 0) return Result<T>.Fail(_things[0]);
            return Result<T>.Successful(creatures[Random.Range(0, creatures.Count)]);
        }
        
        public List<T> GetThingsBasedOnDistance(float distance)
        {
            return GetThingsByDistribution(distance);
        }

        private List<T> GetThingsByDistribution(float distance)
        {
            foreach (var distribution in _distanceDistribution.DistanceDistributions)
            {
                if (distribution.IsInRangeXToY(distance))
                {
                    return GetThingsByDistribution(distribution);
                }
            }
            return null;
        }
        
        private List<T> GetThingsByDistribution(Vector2 distributionArea)
        {
            var creatures = new List<T>();
            foreach (var thing in _things)
            {
                if (distributionArea.IsInRangeXToY(thing.DistanceValue)) 
                    creatures.Add(thing);
            }

            return creatures;
        }
    }
}