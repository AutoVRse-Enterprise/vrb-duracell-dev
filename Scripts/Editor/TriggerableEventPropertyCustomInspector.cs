using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerableEventProperty))]
public class TriggerableEventPropertyCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		TriggerableEventProperty script = (TriggerableEventProperty)target;
		if (GUILayout.Button("InvokeEvent"))
		{
			script.InvokeEvent();
		}
	}
}
