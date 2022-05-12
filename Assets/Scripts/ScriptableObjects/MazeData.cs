using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MazeData", menuName = "ScriptableObjects/MazeData", order = 1)]
    public class MazeData : ScriptableObject
    {
        public Vector2Int size;
    }
}
