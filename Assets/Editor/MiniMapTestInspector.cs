using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MiniMapTest))]
public class MiniMapTestInspector : Editor
{
    private MiniMapTest _thisMiniMapTest;
    public override void OnInspectorGUI()
    {
        _thisMiniMapTest = target as MiniMapTest;
        DrawDefaultInspector();

        if (GUILayout.Button("Add Children"))
        {
            _thisMiniMapTest.MakeChildren();
        }
        
        if (GUILayout.Button("Delete All Children"))
        {
            _thisMiniMapTest.DeleteAllChildren();
        }
    }
}