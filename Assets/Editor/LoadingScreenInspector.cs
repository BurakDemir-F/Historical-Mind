using System;
using UI;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    [CustomEditor(typeof(LoadingScreen))]
    public class LoadingScreenInspector : Editor
    {
        private LoadingScreen _loadingScreen;

        private void OnEnable()
        {
            _loadingScreen = target as LoadingScreen;
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();
            if (GUILayout.Button("Fade In"))
            {
                _loadingScreen.PlayFadeIn();
            }
            
            if (GUILayout.Button("Fade Out"))
            {
                _loadingScreen.PlayFadeOut();
            }
        }
    }
}
