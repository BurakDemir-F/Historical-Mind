using System;
using Maze;
using Patterns;
using UnityEngine;

namespace Managers
{
    public class GameManager : Singleton<GameManager>
    {
        private void Start()
        {
            MazeGenerator.Instance.InitializeData();
            var generationCor = MazeGenerator.Instance.GenerateMaze();
            StartCoroutine(generationCor);
        }
    }
}
