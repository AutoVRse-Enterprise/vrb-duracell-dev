using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(GrabbedHandheldPointerHandler))]
public class GrabbedHandheldPointerHandlerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		GrabbedHandheldPointerHandler script = (GrabbedHandheldPointerHandler)target;
		if (GUILayout.Button("FreeGrabbedHand"))
		{
			script.FreeGrabbedHand();
		}
		if (GUILayout.Button("AddControllerToGrabbedHand"))
		{
			script.AddControllerToGrabbedHand();
		}
	}
}
