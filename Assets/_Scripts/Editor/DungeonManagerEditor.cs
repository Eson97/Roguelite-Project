using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(DungeonManager))]
public class DungeonManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(5);

        var script = (DungeonManager)target;

        if (GUILayout.Button("Generate new dungeon"))
        {
            script.GenerateNewDungeon();
        }
    }
}
