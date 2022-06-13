using System.Collections.Generic;
using UnityEngine;

namespace MazeWorld
{
    public class Locator : MonoBehaviour
    {
        [SerializeField] private List<Transform> positions;
        private int _counter = 0;

        public Vector3 GetPosition()
        {
            return _counter >= positions.Count ? positions[^1].position : positions[_counter++].position;
        }

    }
}
