using System;
using Maze;
using UnityEngine;

namespace Test
{
    public class MarkerTest : MonoBehaviour
    {

        [SerializeField] private LayerMask miniMapMask;
        
        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.M))
            {
                var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
                var pos = MazeInfo.GetRooms()[MazeInfo.currentRoomPosition].transform.position;
                
                cube.transform.position = new Vector3(pos.x, pos.y + 1,pos.z);
                var _renderer = cube.GetComponent<MeshRenderer>();
                _renderer.material.color = Color.magenta;
                cube.layer = LayerMask.NameToLayer("MiniMap") ;
            }
        }
    }
}