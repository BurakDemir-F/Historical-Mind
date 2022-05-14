using MiniMapScripts;
using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "MiniMapCellData", menuName = "ScriptableObjects/MiniMapCellData")]
    public class MiniMapCellData : ScriptableObject
    {
        public MiniMapCell prefab;
        public float rememberingTime;
    }
}
