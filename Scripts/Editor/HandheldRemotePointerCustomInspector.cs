using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(HandheldRemotePointer))]
public class HandheldRemotePointerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		HandheldRemotePointer script = (HandheldRemotePointer)target;
		if (GUILayout.Button("ICastRay"))
		{
			script.CastRay();
		}
	}
}
