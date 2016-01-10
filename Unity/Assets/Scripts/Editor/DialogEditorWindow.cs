using UnityEngine;
using UnityEditor;
public class DialogTreeSettings : EditorWindow
{

	[MenuItem("Window/DialogTreeManager")]
	static void Init()
	{
		DialogTreeSettings window = (DialogTreeSettings)EditorWindow.GetWindow(typeof(DialogTreeSettings));
		window.Show();
	}

	void OnGUI()
	{
		GUILayout.Label("Settings", EditorStyles.boldLabel);
		
	}
}