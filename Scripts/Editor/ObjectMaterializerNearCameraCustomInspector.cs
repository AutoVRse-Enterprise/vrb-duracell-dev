using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(ObjectMaterializerNearCamera))]
public class ObjectMaterializerNearCameraCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		ObjectMaterializerNearCamera script = (ObjectMaterializerNearCamera)target;
		if (GUILayout.Button("MaterializeObjectNearCamera"))
		{
			//script.MaterializeObjectNearCamera();
		}
		if (GUILayout.Button("MaterializeObjectInOriginalPosition"))
		{
			script.MaterializeObjectInOriginalPosition(script.testObj.gameObject);
		}
		if (GUILayout.Button("RotateObject"))
		{
			script.RotateObject(script.testObj.gameObject);
		}
		if (GUILayout.Button("StopRotatingObject"))
		{
			script.StopRotatingObject();
		}
	}
}
