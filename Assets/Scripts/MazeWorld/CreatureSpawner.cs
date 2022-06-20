using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Maze;
using Patterns;
using ScriptableObjects;
using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

namespace MazeWorld
{
    public class CreatureSpawner : Singleton<CreatureSpawner>, IRunOnFrames
    {
        [SerializeField] private CreatureLivingThingDict creatureDictionary;
        [SerializeField] private DistanceDistribution distanceDistribution;
        private List<MazeWorldCreatures> _creatureKeys = new ();
        private DistanceThingPicker<LivingThing> _typeFinder;

        public override void Awake()
        {
            base.Awake();
            Initialize();
        }

        private void Initialize()
        {
            _typeFinder = new DistanceThingPicker<LivingThing>(distanceDistribution, creatureDictionary.Values.ToList());
        }
        
        public Coroutine PerformCor(int waitStep, YieldInstruction wait)
        {
            return StartCoroutine(CreateMonsters(waitStep,wait));
        }
        
        private IEnumerator CreateMonsters(int waitStep, YieldInstruction wait)
        {
            var rooms = MazeInfo.GetRooms();
            var counter = 0;
            foreach (var room in rooms)
            {
                if (++counter % waitStep == 0) yield return wait;
                
                var roomData = (MazeInfo.GetRoomData(room.Value));
                if (!roomData.IsBomb) continue;
                var distanceToGoal = MazeInfo.GetDistanceToGoalCell(roomData.Cell);
                var creatureType = GetCreatureType(distanceToGoal);
                roomData.AddCreature(creatureType);
                CreateMonster(creatureType,room.Value.Locator.GetPosition(),room.Value);
            }
        }

        private MazeWorldCreatures GetCreatureType(float distance)
        {
            var result = _typeFinder.GetThingBasedOnDistance(distance);
            if (!result.IsSuccessful) return MazeWorldCreatures.None;
            return result.Value.Kind;
        }
        
        [Obsolete]
        private MazeWorldCreatures GetRandomCreature()
        {
            var creatureKeys = GetCreatureKeyList();
            var random = Random.Range(0, creatureKeys.Count);
            return creatureKeys[random];
        }
        
        private void CreateMonster(MazeWorldCreatures creatureType, Vector3 pos,RoomBehaviour room)
        {
            if(creatureType == MazeWorldCreatures.None) return;
            
            var creature = Instantiate(creatureDictionary[creatureType].Flesh, room.transform);
            creature.transform.position = pos;
            creature.transform.SetParent(room.transform);
        }

        private List<MazeWorldCreatures> GetCreatureKeyList()
        {
            if (_creatureKeys.Count == 0)
                return _creatureKeys = creatureDictionary.Keys.ToList();
            
            return _creatureKeys;
        }
    }

    [System.Serializable]
    public class CreatureLivingThingDict : SerializableDictionary<MazeWorldCreatures,LivingThing>{}
}
