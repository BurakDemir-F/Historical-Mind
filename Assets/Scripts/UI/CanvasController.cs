using System;
using Managers;
using Patterns;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UI
{
    public class CanvasController : Singleton<CanvasController>
    {
        [SerializeField]private Canvas cursorCanvas,healthCanvas,loadingCanvas;

        public override void Awake()
        {
            base.Awake();
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