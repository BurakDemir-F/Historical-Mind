using System;
using Managers;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CanvasController : MonoBehaviour
    {
        [SerializeField]private Canvas cursorCanvas,healthCanvas,loadingCanvas;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            SceneController.OnSceneChanged += SceneChangedHandler;
        }

        private void OnDestroy()
        {
            SceneController.OnSceneChanged -= SceneChangedHandler;
        }

        private void SceneChangedHandler(Scene scene)
        {
            var isMainMenu = SceneController.IsMainMenu;
            cursorCanvas.gameObject.SetActive(!isMainMenu);
            healthCanvas.gameObject.SetActive(!isMainMenu);
        }
    }
}