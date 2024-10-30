using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(EasySceneLoader))]
public class EasySceneLoaderCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		EasySceneLoader script = (EasySceneLoader)target;
		if (GUILayout.Button("LoadArtAndDevScene"))
		{
			script.LoadArtAndDevScene();
		}
		if (GUILayout.Button("LoadMenuScene"))
		{
			script.LoadMenuScene();
		}
		if (GUILayout.Button("LoadOnlyDevScene"))
		{
			script.LoadOnlyDevScene();
		}
		if (GUILayout.Button("LoadOnlyArtScene"))
		{
			script.LoadOnlyArtScene();
		}
	}
}
