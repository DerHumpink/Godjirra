using System.Collections.Generic;
using System.Linq;
using Editor;
using JetBrains.Annotations;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Editor
{
	public class MenuItems
	{
		[MenuItem("Stuff/CreateSettingsObjects")]
		private static void DoSomething()
		{
			ParameterSetup.CreateIfNotExisting();
			DialogTree.CreateIfNotExisting();
		}

		[MenuItem("Stuff/SaveSetup")]
		private static void SaveDialog()
		{
			DialogTree tree=new DialogTree();
			tree.Stages=DialogStageBehaviour.AllBehaviours.Select(p=>p.Stage).ToList();
			Debug.Log(JsonUtility.ToJson(tree));
			EditorPrefs.SetString("DialogTreeJSON", JsonUtility.ToJson(tree));
		}
	}
}