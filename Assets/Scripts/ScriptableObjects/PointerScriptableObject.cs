using MazeWorld;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "PointerScriptableObject", menuName = "ScriptableObjects/Pointer")]
    public class PointerScriptableObject : ScriptableObject
    {
        [SerializeField] public MazeWorldObjectType type;
        [SerializeField] public MazeWorldCreatures creatureType;
        [SerializeField] public MazeWorldObjects mazeObject;

        [SerializeField] public GameObject prefab;
    }
}
