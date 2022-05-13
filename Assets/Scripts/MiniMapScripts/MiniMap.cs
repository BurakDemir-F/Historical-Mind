using System.Collections.Generic;
using ScriptableObjects;
using UnityEngine;

namespace MiniMapScripts
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField] private MiniMapCellData miniMapData;
        private List<MiniMapCell> _miniMapCells;
    }
}
