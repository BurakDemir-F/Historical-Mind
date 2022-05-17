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
            // var generationCor = MazeGenerator.Instance.GenerateMaze();
            // StartCoroutine(generationCor);
            
            MazeGenerator.Instance.GenerateMaze();
        }

        public void StartGame()
        {
            MazeGenerator.Instance.InitializeData();
            // var generationCor = MazeGenerator.Instance.GenerateMaze();
            // StartCoroutine(generationCor);
            
            MazeGenerator.Instance.GenerateMaze();
        }
    }
}
