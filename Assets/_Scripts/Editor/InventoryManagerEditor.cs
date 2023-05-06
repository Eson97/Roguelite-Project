using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(InventoryManager))]
public class InventoryManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GUILayout.Space(5);

        var script = (InventoryManager)target;

        if (GUILayout.Button("Add Seed"))
        {
            script.AddItem(script.TestSeedSO, 10);
        }
        if (GUILayout.Button("Add Crop"))
        {
            script.AddItem(script.TestCropSO,10);
        }
        if (GUILayout.Button("Remove Seed"))
        {
            script.RemoveItem(script.TestSeedSO, 10);
        }
        if (GUILayout.Button("Remove Crop"))
        {
            script.RemoveItem(script.TestCropSO, 10);
        }

    }
}