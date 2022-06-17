using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Algorithms;
using Maze;
using Patterns;
using UnityEngine;
using Utilities;

namespace MazeWorld
{
    public class CreatureManager : Singleton<CreatureManager>, IRunOnFrames
    {
        [SerializeField] private CreatureLivingThingDict creatureDictionary;
        private List<MazeWorldCreatures> _creatureKeys = new List<MazeWorldCreatures>();

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
                //print($"distance to goal:{distanceToGoal}");
                if (distanceToGoal > 0.7f) continue;
                CreateMonster(GetRandomCreature(),room.Value.Locator.GetPosition(),room.Value);
            }
        }

        private MazeWorldCreatures GetRandomCreature()
        {
            var creatureKeys = GetCreatureKeyList();
            var random = Random.Range(0, creatureKeys.Count);
            return creatureKeys[random];
        }
        
        private void CreateMonster(MazeWorldCreatures creatureType, Vector3 pos,RoomBehaviour room)
        {
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
