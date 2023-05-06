using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GameManager))]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(5);

        var script = (GameManager)target;

        if (GUILayout.Button("Test Save"))
        {
            script.Save();
        }
        if (GUILayout.Button("Test Load"))
        {
            script.Load();
        }
    }
}
