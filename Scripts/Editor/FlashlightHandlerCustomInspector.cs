using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FlashlightHandler))]
public class FlashlightHandlerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		FlashlightHandler script = (FlashlightHandler)target;
		if (GUILayout.Button("EnableFlashlight"))
		{
			script.EnableFlashlight();
		}
		if (GUILayout.Button("DisableFlashlight"))
		{
			script.DisableFlashlight();
		}
	}
}
