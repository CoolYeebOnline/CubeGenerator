using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor((typeof(gridgeneration)))]
public class gridEditor : Editor
{
    public override void OnInspectorGUI()
    {
        gridgeneration gridGen = (gridgeneration)target;
        base.OnInspectorGUI();
        GUILayout.Label("Apply X,Y,Z");
        if(GUILayout.Button("Generate"))
        {
            gridGen.StartGen();
            Debug.Log("Generating Grid...");
        }
        if (GUILayout.Button("Delete"))
        {
            gridGen.Delete();
            Debug.Log("Grid Deleted!");
        }
    }
}
