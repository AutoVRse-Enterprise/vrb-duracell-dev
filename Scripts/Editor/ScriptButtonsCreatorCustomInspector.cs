using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ScriptButtonsCreator))]
public class ScriptButtonsCreatorCustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ScriptButtonsCreator buttonsCreator = (ScriptButtonsCreator)target;
        if(GUILayout.Button("Get functions"))
        {
            buttonsCreator.GetFunctions();
        }
        if (GUILayout.Button("Generate Buttons"))
        {
            buttonsCreator.GenerateButtons();
        }
    }
}
