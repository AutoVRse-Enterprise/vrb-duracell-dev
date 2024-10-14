using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(RCCarSFXHandler))]
public class RCCarSFXHandlerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		RCCarSFXHandler script = (RCCarSFXHandler)target;
		if (GUILayout.Button("PlaySegment"))
		{
			script.PlaySegment(script.startTime, script.endTime);
		}
	}
}
