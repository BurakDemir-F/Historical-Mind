using System;
using UI;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    [CustomEditor(typeof(BFadeManager))]
    public class BImageManagerInspector : Editor
    {
        private BFadeManager _fadeManager;

        private void OnEnable()
        {
            _fadeManager = target as BFadeManager;
        }

        public override void OnInspectorGUI()
        {

            DrawDefaultInspector();
            if (GUILayout.Button("Fade In"))
            {
                _fadeManager.PlayFadeInAnimation();
            }
            
            if (GUILayout.Button("Fade Out"))
            {
                _fadeManager.PlayFadeOutAnimation();
            }
        }
    }
}
