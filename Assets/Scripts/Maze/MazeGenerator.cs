using System;
using System.Collections;
using System.Collections.Generic;
using Algorithms;
using Test;
using UnityEngine;

namespace Maze
{
    public class MazeGenerator : MonoBehaviour
    {
        [SerializeField] private RoomBehaviour mazeRoom;
        [SerializeField] private float roomOffset = 7f;
        
        private Vector2Int _size = MazeInfo.size;
        private List<Cell> _cells;

        private void Start()
        {
            var backTracing = new BackTracing(_size.x, _size.y);
            _cells = backTracing.GetCells();

            StartCoroutine(GenerateMaze(_cells));
        }

        private IEnumerator GenerateMaze(List<Cell> cells)
        {
            var waitForSeconds = new WaitForSeconds(0.01f);
            foreach (var cell in cells)
            {
                if (!cell.IsVisited) continue;

                var newRoom = Instantiate(mazeRoom, 
                    new Vector3(cell.Position.x * roomOffset, 0f, -cell.Position.y * roomOffset),
                    Quaternion.identity);
                newRoom.name = $"Room: {cell.Position.x} : {cell.Position.y}";
                newRoom.UpdateRoom(cell.GetNeighborStatuses());
                newRoom.SetRoomPosition(cell.Position);
                MazeInfo.AddRoom(cell.Position,newRoom);
                yield return waitForSeconds;
            }

            yield return new WaitForSeconds(3f);

            #region test

            var player = new GameObject("Player");
            player.AddComponent<BasicPlayer>();
            player.tag = "Player";
            player.AddComponent<BoxCollider>();
            var rigidBody = player.AddComponent<Rigidbody>();
            rigidBody.useGravity = false;
            player.transform.position = new Vector3(0, 2f, 0);
            
            if (MazeInfo.TryGetRoom(new Vector2Int(0,0), out mazeRoom))
            {
                mazeRoom.OnTriggerEnter(player.GetComponent<BoxCollider>());    
            }
            
            var cam = Camera.main;
            cam.transform.position = new Vector3(0, 1f, -0.5f);
            cam.transform.SetParent(player.transform);
            cam.transform.rotation = Quaternion.Euler(Vector3.zero);
            #endregion

        }
    }
}
