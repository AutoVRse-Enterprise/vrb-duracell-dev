using UnityEditor;
using UnityEngine;
using VRseBuilder.Core.Utility;
[CustomEditor(typeof(ObjectHighlighter))]
public class ObjectHighlighterCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		ObjectHighlighter script = (ObjectHighlighter)target;
		if (GUILayout.Button("EnableHighlight"))
		{
			script.EnableHighlight();
		}
		if (GUILayout.Button("DisableHighlight"))
		{
			script.DisableHighlight();
		}
	}
}
