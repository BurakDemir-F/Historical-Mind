using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MazeData", menuName = "ScriptableObjects/MazeData")]
    public class MazeData : ScriptableObject
    {
        public Vector2Int size;
    }
}
