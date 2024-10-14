using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
using System.Reflection;
using UnityEditor;

public class ScriptButtonsCreator : MonoBehaviour
{
    [SerializeField]
    private ClassFunctionData[] _classFunctionData;
    [SerializeField]
    private Component _targetComponent;
    [SerializeField]
    private string _editorFolderLocation;
    private string _className;
    public void GetFunctions()
    {
        if (_targetComponent == null)
        {
            Debug.LogWarning("Target component is not assigned.");
            return;
        }

        Type componentType = _targetComponent.GetType();
        _className = componentType.Name;
        MethodInfo[] methods = componentType.GetMethods(BindingFlags.Public | BindingFlags.Instance | BindingFlags.DeclaredOnly);
        _classFunctionData = new ClassFunctionData[methods.Length];
        for(int i = 0; i < _classFunctionData.Length; i++)
        {
            _classFunctionData[i] = new ClassFunctionData(methods[i].Name);
        }
    }
    public void GenerateButtons()
    {
        string scriptContent = "using UnityEditor;\n";
        scriptContent += "using UnityEngine;\n\n";
        scriptContent += "[CustomEditor(typeof(" + _className + "))]\n";
        string scriptName = _className + "CustomInspector";
        scriptContent += "public class " + scriptName + " : Editor\n";
        scriptContent += "{\n";
        scriptContent += "\tpublic override void OnInspectorGUI()\n";
        scriptContent += "\t{\n";
        scriptContent += "\t\tbase.OnInspectorGUI();\n";
        scriptContent += "\t\t" + _className + " script = (" + _className + ")target;\n";
        
        for(int i = 0; i < _classFunctionData.Length; i++)
        {
            if (_classFunctionData[i].shouldMakeFunction)
            {
                string scriptButton = "";
                scriptButton += "\t\tif (GUILayout.Button(\"" + _classFunctionData[i].functionName + "\"))\n";
                scriptButton += "\t\t{\n";
                scriptButton += "\t\t\tscript." + _classFunctionData[i].functionName + "();\n";
                scriptButton += "\t\t}\n";
                scriptContent += scriptButton;
            }
        }
        scriptContent += "\t}\n";
        scriptContent += "}\n";
        string filePath = _editorFolderLocation + @"\" + scriptName + ".cs";
        File.WriteAllText(filePath, scriptContent);
        string relativePath = filePath[filePath.IndexOf("Assets")..];
        Debug.Log("Editor script generated: " + filePath);

    }
}

[System.Serializable]
public class ClassFunctionData
{
    public string functionName;
    public bool shouldMakeFunction;
    public ClassFunctionData(string functionName)
    {
        this.functionName = functionName;
    }
}
