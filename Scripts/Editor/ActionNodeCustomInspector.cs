using UnityEngine;
using UnityEditor;
using VRseBuilder.Core.NoCode;
using System.Collections.Generic;

[CustomEditor(typeof(Node))]
public class ActionNodeCustomInspector : Editor
{
    private SerializedObject serializedObjectRef;
    private SerializedProperty nodesProperty;

    private void OnEnable()
    {
        serializedObjectRef = new SerializedObject(target);
    }
    public override void OnInspectorGUI()
    {
        serializedObjectRef.Update();
        if (GUILayout.Button("Hi"))
        {
            Debug.Log("hi");
        }
    }
}
