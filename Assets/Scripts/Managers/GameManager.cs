using System.Collections;
using Maze;
using MazeWorld;
using MazeWorld.Dedalus;
using UI;
using UnityEngine;

namespace Managers
{
    public class GameManager : Patterns.Singleton<GameManager>
    {
        [SerializeField] private int waitStep = 10;

        private IEnumerator Start()
        {
            MazeGenerator.Instance.InitializeData();
            MazeGenerator.Instance.GenerateBackTracking();
            yield return StartCoroutine(MazeGenerator.Instance.SetBombs());
            yield return StartCoroutine(MazeGenerator.Instance.GenerateMaze());

            var wait = new WaitForEndOfFrame();
            yield return CreatureSpawner.Instance.PerformCor(waitStep, wait);
            yield return DedalusBoxSpawner.Instance.PerformCor(waitStep, wait);

            var startCell = MazeGenerator.Instance.StartCell;
            var startVector = new Vector2Int(startCell.Position.x, startCell.Position.y);
            StartCoroutine(MazeGenerator.Instance.CreateTestPlayer(startVector));
            TestAudioPlayer.Instance.PlayMusic();
            PlayFades();
        }

        private void PlayFades()
        {
            var fadeManagers = FindObjectsOfType<BFadeManager>();
            print($"fade managers count{fadeManagers.Length}");
            foreach (var bFadeManager in fadeManagers)
            {
                bFadeManager.PlayFadeInAnimation();
            }
        }
    }
}
