using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
	[CustomEditor(typeof (ParameterSetup))]
	public class ParameterSetupInspector : UnityEditor.Editor
	{
		private bool _unlockIds;
		private bool[] foldoutStatus;

		private readonly string[] ParameterTypes = {"Number", "Boolean"};

		public override void OnInspectorGUI()
		{
			var parameterSetup = target as ParameterSetup;

			if (foldoutStatus == null || foldoutStatus.Length < parameterSetup.Parameters.Count)
				foldoutStatus = new bool[parameterSetup.Parameters.Count];

			for (var i = 0; i < parameterSetup.Parameters.Count; i++)
			{
				var parameter = parameterSetup.Parameters[i];
				foldoutStatus[i] = EditorGUILayout.Foldout(foldoutStatus[i], parameter.Name);
				if (foldoutStatus[i])
				{
					EditorGUILayout.BeginHorizontal();
					SetID(parameter, parameterSetup);
					parameter.Name = EditorGUILayout.TextField(parameter.Name);

					SetType(parameter);

					if (GUILayout.Button("X", GUILayout.Width(30)))
					{
						parameterSetup.RemoveParameter(parameter);
					}
					EditorGUILayout.EndHorizontal();
				}
			}

			EditorGUILayout.Space();
			if (GUILayout.Button("Add"))
			{
				parameterSetup.AddParameter();
			}

			if (GUI.changed)
			{
				EditorUtility.SetDirty(target);
			}
		}


		private void SetID(Parameter parameter, ParameterSetup parameterSetup)
		{
			if (!_unlockIds)
			{
				EditorGUILayout.LabelField("ID: " + parameter.Id, GUILayout.Width(45));
			}
			else
			{
				var id = EditorGUILayout.IntField(parameter.Id, GUILayout.Width(45));
				if (parameterSetup.Parameters.Any(p => p.Id == id))
					return;

				parameter.Id = id;
			}
		}

		private void SetType(Parameter parameter)
		{
			parameter.Type=(ParameterType)EditorGUILayout.Popup((int) parameter.Type, ParameterTypes,GUILayout.Width(100));
		}
	}
}