using System;
using System.Collections.Generic;
using Maze;
using ScriptableObjects;
using UnityEngine;

namespace MiniMapScripts
{
    public class MiniMap : MonoBehaviour
    {
        [SerializeField] private MiniMapCellData miniMapData;
        [SerializeField] private float offset;
        [SerializeField] private Camera miniMapCamera;
        private Dictionary<Vector2Int,MiniMapCell> _miniMapCells;

        private void Start()
        {
            _miniMapCells = new Dictionary<Vector2Int, MiniMapCell>();
            RoomEnterBehaviour.ONRoomEntered += OnRoomEntered;
        }

        private void OnDestroy()
        {
            RoomEnterBehaviour.ONRoomEntered -= OnRoomEntered;
        }

        private void OnRoomEntered(RoomBehaviour room, Collider other, RoomBehaviour up,RoomBehaviour down,RoomBehaviour right, RoomBehaviour left)
        {
            CheckAndAddCell(room.GetRoomPosition());
        }

        private void CheckAndAddCell(Vector2Int position)
        {
            if (!_miniMapCells.ContainsKey(position))
            {
                var mazeCell = Instantiate(miniMapData.prefab,transform,false);
                mazeCell.transform.position = new Vector3(position.x * offset, -100f, -position.y * offset);
                mazeCell.SetMiniMapCell(position, miniMapData.rememberingTime);
                mazeCell.name = $"{position.x} : {position.y}";
                _miniMapCells.Add(position,mazeCell);
                return;
            }

            var oldCell = _miniMapCells[position];
            
            oldCell.SetRememberingTime(miniMapData.rememberingTime);

            if(!oldCell.IsForgotten) return;
            
            oldCell.gameObject.SetActive(true);
        }
    }
}
