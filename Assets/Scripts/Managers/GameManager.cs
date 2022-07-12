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
        public bool IsPlayable { get; private set; } = false;
        
        
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
            yield return StartCoroutine(MazeGenerator.Instance.CreateTestPlayer(startVector));
            // LoadingScreen.Instance.PlayFadeOut();
            TestAudioPlayer.Instance.PlayMusic();
            yield return new WaitForSeconds(2f);
            LoadingScreen.Instance.PlayFadeOut();
            PlayFades();
        }

        private void PlayFades()
        {
            FadeManagerItemHolder.PlayFadeIn();
        }
    }
}
