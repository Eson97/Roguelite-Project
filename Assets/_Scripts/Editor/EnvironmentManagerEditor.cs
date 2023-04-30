using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(EnvironmentManager))]
public class EnvironmentManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(5);

        var script = (EnvironmentManager)target;

        if(GUILayout.Button("Next DayTime"))
        {
            script.NextDayTime();
        }
        GUILayout.Space(3);

        if(GUILayout.Button("Next WeekDay"))
        {
            script.NextWeekDay();
        }
    }
}
