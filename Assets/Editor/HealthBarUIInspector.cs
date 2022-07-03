using CodeMonkey.HealthSystemCM;
using UnityEditor;
using UnityEngine;
using Utilities;

namespace Editors
{
    [CustomEditor(typeof(HealthBarUI))]
    public class HealthBarUIInspector : Editor
    {
        private HealthBarUI _healthBarUI;
        
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Heal"))
            {
                _healthBarUI = target as HealthBarUI;
                if (_healthBarUI.IsNotNull())
                {
                    _healthBarUI.GetHealthSystem().HealComplete();
                }
            }
        }
    }
}
