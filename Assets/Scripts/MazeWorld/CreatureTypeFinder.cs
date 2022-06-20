using System;
using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace MazeWorld
{
    // this class gets creatures based on its distance value to goal cell
    public class CreatureTypeFinder
    {
        private readonly CreatureDistanceDistribution _distanceDistribution;
        private readonly CreatureLivingThingDict _creatureDictionary;
        private readonly Vector2 _starter;
        private readonly Vector2 _regular;
        private readonly Vector2 _boss;

        private readonly List<MazeWorldCreatures> _starterCreatures;
        private readonly List<MazeWorldCreatures> _regularCreatures;
        private readonly List<MazeWorldCreatures> _bossCreatures;
        
        public CreatureTypeFinder(CreatureDistanceDistribution distanceDistribution, CreatureLivingThingDict creatureDictionary)
        {
            _distanceDistribution = distanceDistribution;
            _creatureDictionary = creatureDictionary;
            _starter = _distanceDistribution.Starter;
            _regular = _distanceDistribution.Regular;
            _boss = _distanceDistribution.Boss;

            _starterCreatures = GetMonstersByDistribution(_starter);
            _regularCreatures = GetMonstersByDistribution(_regular);
            _bossCreatures = GetMonstersByDistribution(_boss);
        }

        public MazeWorldCreatures GetCreatureType(float distance)
        {
            var creatures = GetCreatureTypes(distance);
            if (creatures == null || creatures.Count == 0) return MazeWorldCreatures.None;
            return creatures[Random.Range(0, creatures.Count)];
        }
        
        public List<MazeWorldCreatures> GetCreatureTypes(float distance)
        {
            return GetMonstersByDistribution(distance);
        }

        private List<MazeWorldCreatures> GetMonstersByDistribution(float distance)
        {
            if (_starter.IsInRangeXToY(distance)) return _starterCreatures;
            if (_regular.IsInRangeXToY(distance)) return _regularCreatures;
            if (_boss.IsInRangeXToY(distance)) return _bossCreatures;
            return null;
        }
        
        private List<MazeWorldCreatures> GetMonstersByDistribution(Vector2 distributionArea)
        {
            var creatures = new List<MazeWorldCreatures>();
            foreach (var creaturePair in _creatureDictionary)
            {
                if (distributionArea.IsInRangeXToY(creaturePair.Value.DangerValue)) 
                    creatures.Add(creaturePair.Key);
            }

            return creatures;
        }
    }
}