using System;
using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Managers
{
    public class SceneController : Singleton<SceneController>
    {
        public static Scene CurrentScene => SceneManager.GetActiveScene();
        public static bool IsMainMenu => CurrentScene.name.Contains("Main",StringComparison.OrdinalIgnoreCase);
        public static bool IsMaze => CurrentScene.name.Contains("Historical", StringComparison.OrdinalIgnoreCase);

        public static event Action<Scene> OnSceneChanged; 

        public override void Awake()
        {
            base.Awake();
            SceneManager.sceneLoaded += SceneChangedHandler;
        }

        private void OnDestroy()
        {
            SceneManager.sceneLoaded -= SceneChangedHandler;
        }

        private void SceneChangedHandler(Scene loadedScene, LoadSceneMode loadSceneMode)
        {
            OnSceneChanged?.Invoke(loadedScene);
        }
    }
}
