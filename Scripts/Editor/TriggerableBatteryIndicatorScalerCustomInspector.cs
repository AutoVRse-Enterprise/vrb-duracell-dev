using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(TriggerableBatteryIndicatorScaler))]
public class TriggerableBatteryIndicatorScalerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		TriggerableBatteryIndicatorScaler script = (TriggerableBatteryIndicatorScaler)target;
		if (GUILayout.Button("TempFunc"))
		{
			script.TempFunc();
		}
	}
}
