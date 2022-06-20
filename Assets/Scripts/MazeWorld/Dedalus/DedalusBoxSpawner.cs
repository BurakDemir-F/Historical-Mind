using System.Collections;
using Algorithms;
using Maze;
using Patterns;
using UnityEngine;

namespace MazeWorld.Dedalus
{
    public class DedalusBoxSpawner : Singleton<DedalusBoxSpawner>, IRunOnFrames
    {
        [SerializeField] private GameObject dedalusBoxPrefab;

        public Coroutine PerformCor(int waitStep, YieldInstruction wait)
        {
            return StartCoroutine(SpawnBoxes(waitStep,wait));
        }

        private IEnumerator SpawnBoxes(int waitStep, YieldInstruction wait)
        {
            var rooms = MazeInfo.GetRooms();
            var counter = 0;
            foreach (var roomPair in rooms)
            {
                var roomData = MazeInfo.GetRoomData(roomPair.Value);
                if(roomData.IsBomb) continue;
                var newDedaluxBox = SpawnBox(roomPair.Value.Locator.GetPosition(),roomPair.Value.transform);
                newDedaluxBox.SetRoom(roomPair.Value);
                if (++counter % waitStep == 0) yield return wait;
            }
        }

        private DedalusBox SpawnBox(Vector3 position, Transform parent)
        {
            var newDedalusBox = Instantiate(dedalusBoxPrefab, parent);
            newDedalusBox.transform.position = position;
            return newDedalusBox.GetComponent<DedalusBox>();
        }
    }
}
