using System;
using Maze;
using Unity.VisualScripting;
using UnityEditor.TerrainTools;

namespace Managers
{
    public class GameManager : Patterns.Singleton<GameManager>
    {
        private void Start()
        {
            MazeGenerator.Instance.InitializeData();
            MazeGenerator.Instance.GenerateBackTracking();
            MazeGenerator.Instance.SetBombs();
            MazeGenerator.Instance.GenerateMaze();
        }
    }
}
