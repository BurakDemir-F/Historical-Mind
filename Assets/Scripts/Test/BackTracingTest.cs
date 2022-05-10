using System.Collections.Generic;
using Algorithms;
using UnityEngine;
using UnityEngine.UI;

namespace Test
{
    public class BackTracingTest : MonoBehaviour
    {
        private static Color _red = Color.red;
        
        public void CreateTestVisuals(List<Cell> cells,Vector2Int size, float offset)
        {

            foreach (var cell in cells)
            {
                if (!cell.IsVisited) continue;

                    var newCube = CreateCube(new Vector3(cell.Position.x * offset, 2,-cell.Position.y * offset),
                    Vector3.one * 1.3f,
                    cell.IsVisited,
                    null,
                    $"X: {cell.Position.x} Y: {cell.Position.y}");

                var neighborStatuses = cell.GetNeighborStatuses();

                var newCubePos = newCube.transform.position;
                var cubePosX = newCubePos.x;
                var cubePosY = newCubePos.y;
                var cubePosZ = newCubePos.z;
                
                for (var i = 0; i < neighborStatuses.Count; i++)
                {
                    var isNeighbor = neighborStatuses[i];

                    switch (i)
                    {
                        case 0:
                        {
                            var neighborCube = CreateCube(new Vector3(cubePosX, cubePosY, cubePosZ + 1), Vector3.one * 0.5f,
                                isNeighbor, newCube.transform, newCube.name + "Up");
                            break;
                        }
                        case 1:
                        {
                            var neighborCube = CreateCube(new Vector3(cubePosX, cubePosY, cubePosZ - 1), Vector3.one * 0.5f,
                                isNeighbor, newCube.transform, newCube.name + "Down");
                            break;
                        }
                        case 2:
                        {
                            var neighborCube = CreateCube(new Vector3(cubePosX + 1, cubePosY, cubePosZ), Vector3.one * 0.5f,
                                isNeighbor, newCube.transform, newCube.name + "Right");
                            break;
                        }
                        case 3:
                        {
                            var neighborCube = CreateCube(new Vector3(cubePosX - 1, cubePosY, cubePosZ), Vector3.one * 0.5f,
                                isNeighbor, newCube.transform, newCube.name + "Left");
                            break;
                        }
                    }
                }
            }
        }

        private GameObject CreateCube(Vector3 position,Vector3 scale, bool isActive,Transform parent, string cubeName)
        {
            var cube = GameObject.CreatePrimitive(PrimitiveType.Cube);
            cube.SetActive(isActive);
            cube.transform.position = position;
            cube.transform.localScale = scale;
            cube.name = cubeName;
            cube.transform.SetParent(parent);
            cube.GetComponent<MeshRenderer>().material.color = Color.red;
            return cube;
        }
        
        
    }
}
