using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TestScriptableTrigger))]
public class TestScriptableTriggerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		TestScriptableTrigger script = (TestScriptableTrigger)target;
		if (GUILayout.Button("PlayAnim"))
		{
			script.PlayAnim();
		}
	}
}
