using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(PoweredObjectsListHandler))]
public class PoweredObjectsListHandlerCustomInspector : Editor
{
	public override void OnInspectorGUI()
	{
		base.OnInspectorGUI();
		PoweredObjectsListHandler script = (PoweredObjectsListHandler)target;
		if (GUILayout.Button("TickMarkPanel"))
		{
			script.TickMarkPanel(script.index);
		}
        if (GUILayout.Button("UnTickMarkPanel"))
        {
            script.TickMarkPanel(script.index);
        }
        if (GUILayout.Button("SetPanelLocation"))
        {
            script.SetPanelLocation(script.index);
        }
    }
}
