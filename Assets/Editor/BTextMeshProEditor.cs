using TMPro;
using TMPro.EditorUtilities;
using UI;
using UnityEditor;
using UnityEngine;

namespace EditorSpecific
{
    [CustomEditor(typeof(BTextMeshPro))]
    public class BTextMeshProEditor : TMP_EditorPanel
    {
        private BTextMeshPro _bTextMeshPro;

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            _bTextMeshPro = target as BTextMeshPro;
            _bTextMeshPro.alphaStart = EditorGUILayout.FloatField(_bTextMeshPro.alphaStart);
            _bTextMeshPro.alphaEnd = EditorGUILayout.FloatField(_bTextMeshPro.alphaEnd);
        }
    }
}