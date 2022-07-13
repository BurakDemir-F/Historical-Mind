using System;
using System.Collections;
using Maze;
using MazeWorld;
using MazeWorld.Dedalus;
using Storing;
using UI;
using UnityEngine;
using UnityEngine.SceneManagement;
using Utilities;

namespace Managers
{
    public class GameManager : Patterns.Singleton<GameManager>
    {
        [SerializeField] private int waitStep = 10;
        public bool IsPlayable { get; private set; } = false;

        private bool _isLoadGame = false;

        private void Start()
        {
            SceneController.OnSceneChanged += SceneChangedHandler;
        }

        private void OnDestroy()
        {
            SceneController.OnSceneChanged -= SceneChangedHandler;
        }

        private IEnumerator StartNewGame()
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

        private void SceneChangedHandler(Scene loadedScene)
        {
            if (SceneController.IsMaze && !_isLoadGame)
            {
                StartCoroutine(StartNewGame());
            }
            
            if (SceneController.IsMaze && _isLoadGame)
            {
                StartCoroutine(LoadGame());
            }
        }

        private IEnumerator LoadGame()
        {
            yield break;
        }
        
        private void PlayFades()
        {
            FadeManagerItemHolder.PlayFadeIn();
        }

        public void Save()
        {
            var storingClass = new StoringClass(MazeInfo.GetRoomDataList(),
                MazeInfo.currentRoomPosition.ToSerializable(),
                PlayerInfo.PlayerHealth);
            SaveLoad.Save(storingClass);
        }

        public void Load()
        {
            var load = SaveLoad.Load();
            print("storing class");
            print("health: " + load.GetHealthSystem().GetHealth());
            print ("current room" + load.GetCurrentRoomPosition());
            print($"Room data list count: {load.GetRoomDataList()}");
        }
    }
}
