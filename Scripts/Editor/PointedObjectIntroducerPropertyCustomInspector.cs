using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PointedObjectIntroducerProperty))]
public class PointedObjectIntroducerPropertyCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PointedObjectIntroducerProperty script = (PointedObjectIntroducerProperty)target;
		if (GUILayout.Button("IntroduceObject"))
		{
			script.IntroduceObject(script._testObj);
		}
	}
}
