using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(VignetteHandler))]
public class VignetteHandlerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		VignetteHandler script = (VignetteHandler)target;
		if (GUILayout.Button("DarkenArea"))
		{
			script.DarkenArea();
		}
		if (GUILayout.Button("BrightenArea"))
		{
			script.BrightenArea();
		}
	}
}
