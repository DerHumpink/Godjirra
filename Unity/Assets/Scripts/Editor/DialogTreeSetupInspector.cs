
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof(DialogTree))]
	public class DialogTreeInspector : UnityEditor.Editor
	{
		public override void OnInspectorGUI()
		{
			DialogTree tree=target as DialogTree;

			EditorGUILayout.LabelField("Dialog Tree Settings",EditorStyles.boldLabel);
			EditorGUILayout.HelpBox("This container object is carrying data into the game mode",MessageType.Info);

			if (DialogStageBehaviour.AllBehaviours.Length > 0)
			{
				SetFirstStage(tree);
				if (GUILayout.Button("Parse current setup"))
				{
					tree.Stages = DialogStageBehaviour.AllBehaviours.Select(s=>s.Stage).ToList();
					EditorUtility.SetDirty(target);
				}
			}

			EditorGUILayout.LabelField("Current Data: "+tree.Stages.Count+ " Stages");
			EditorGUI.indentLevel++;
			foreach (DialogStage stage in tree.Stages.OrderBy(s=>s.Id))
			{
				EditorGUILayout.LabelField(stage.Name,EditorStyles.helpBox);
			}
		}

		private static void SetFirstStage(DialogTree tree)
		{
			if (DialogStageBehaviour.AllBehaviours.Length > 0)
			{
				var currentFisrstStage = DialogStageBehaviour.AllBehaviours.FirstOrDefault(s => s.Stage.Id == tree.FistStageId);
				var newFisrstStage =
					EditorGUILayout.ObjectField("First Stage", currentFisrstStage, typeof (DialogStageBehaviour), true) as
						DialogStageBehaviour;
				if (newFisrstStage != null)
					tree.FistStageId = newFisrstStage.Stage.Id;
			}
		}
	}
}