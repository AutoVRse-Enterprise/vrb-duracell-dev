using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(IntroduceAllObjectsProperty))]
public class IntroduceAllObjectsPropertyCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		IntroduceAllObjectsProperty script = (IntroduceAllObjectsProperty)target;
		if (GUILayout.Button("IInvokeFinishEvent"))
		{
			script.InvokeFinishEvent();
		}
	}
}
