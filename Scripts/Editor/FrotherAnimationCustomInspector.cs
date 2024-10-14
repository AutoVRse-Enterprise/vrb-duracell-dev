using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(FrotherAnimation))]
public class FrotherAnimationCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		FrotherAnimation script = (FrotherAnimation)target;
		if (GUILayout.Button("PlayFrotherAnimation"))
		{
			script.PlayFrotherAnimation();
		}
	}
}
