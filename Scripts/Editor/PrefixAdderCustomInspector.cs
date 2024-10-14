using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PrefixAdder))]
public class PrefixAdderCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PrefixAdder script = (PrefixAdder)target;
		if (GUILayout.Button("AddPrefixes"))
		{
			script.AddPrefixes();
		}
		if (GUILayout.Button("IterateThroughChildren"))
		{
			script.IterateThroughChildren(script.transform);
		}
	}
}
