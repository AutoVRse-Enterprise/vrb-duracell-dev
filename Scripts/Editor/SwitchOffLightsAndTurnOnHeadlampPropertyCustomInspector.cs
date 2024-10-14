using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SwitchOffLightsAndTurnOnHeadlampProperty))]
public class SwitchOffLightsAndTurnOnHeadlampPropertyCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		SwitchOffLightsAndTurnOnHeadlampProperty script = (SwitchOffLightsAndTurnOnHeadlampProperty)target;
		if (GUILayout.Button("InvokeFinishEvent"))
		{
			script.InvokeFinishEvent();
		}
	}
}
