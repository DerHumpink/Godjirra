using Editor;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof (DialogStageBehaviour))]
public class DialogStageBehaviourEditor : UnityEditor.Editor
{
	public override void OnInspectorGUI()
	{
		var stage = (target as DialogStageBehaviour).Stage;

		//Name
		GUILayout.Label("Stage Name");
		GUI.color = new Color(0.9f, 0.6f, 0.6f);
		stage.Name = GUILayout.TextField(stage.Name);

		//Text
		GUI.color = Color.white;
		GUILayout.Label("Text");
		stage.Text = GUILayout.TextArea(stage.Text, GUILayout.MaxHeight(50));
		GUILayout.Space(10);

		//Answers
		for (var i = 0; i < stage.Answers.Count; i++)
		{
			InspectAnswer(stage.Answers[i], stage);
		}


		if (GUILayout.Button("(+) Add Answer"))
		{
			stage.Answers.Add(new Answer());
		}

		if (GUI.changed)
		{
			EditorUtility.SetDirty(target);
		}
	}

	private void InspectAnswer(Answer answer, DialogStage stage)
	{
		GUILayout.Label("----------------ANSWER--------------", EditorStyles.boldLabel);
		EditorGUI.indentLevel++;
		GUI.color = new Color(0.6f, 0.9f, 0.6f);
		answer.Text = EditorGUILayout.TextField(answer.Text);

		EditorGUI.indentLevel++;

		//Display Ispectors for Conditions, Effects and the next scene of the answer
		DisplayConditions(answer);
		DisplayEffects(answer);
		SelectNextScene(answer, stage);

		GUI.color = Color.white;
		EditorGUI.indentLevel--;
		EditorGUI.indentLevel--;
		EditorGUILayout.Space();
	}


	private void DisplayConditions(Answer answer)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Conditions", EditorStyles.boldLabel);
		GUI.color = Color.gray;
		if (GUILayout.Button("(+)", GUILayout.Width(50)))
		{
			answer.Conditions.Add(new Condition());
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Parameter                       -    Operator - Value");
		if (answer.Conditions.Count > 0)
		{
			for (var i = 0; i < answer.Conditions.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				GUI.color = new Color(0.83f, 0.83f, 0.9f);
				var condition = answer.Conditions[i];

				//Select Parameter
				var selection = ParameterSetup.Instance.GetParameterIndexById(condition.parameterID);
				selection = EditorGUILayout.Popup(selection, ParameterSetup.Instance.GetAllParameterNames(), GUILayout.Width(200));
				condition.parameterID = ParameterSetup.Instance.GetParameterIdByIndex(selection);

				EditorGUI.indentLevel -= 2;
				condition.Operator = (CompareFunction) EditorGUILayout.EnumPopup(condition.Operator, GUILayout.Width(60));

				if (ParameterSetup.Instance.GetParameterById(condition.parameterID).Type == ParameterType.Boolean)
				{
					condition.Value = EditorGUILayout.ToggleLeft("value", condition.Value > 0, GUILayout.Width(100)) ? 1 : 0;
				}
				else
				{
					condition.Value = EditorGUILayout.IntField(condition.Value);
				}

				GUI.color = new Color(0.9f, 0.7f, 0.7f);
				if (GUILayout.Button("X", GUILayout.Width(25)))
				{
					answer.Conditions.Remove(condition);
				}

				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel += 2;
			}
		}
		GUI.color = Color.white;
	}

	private void DisplayEffects(Answer answer)
	{
		EditorGUILayout.BeginHorizontal();
		EditorGUILayout.LabelField("Effect", EditorStyles.boldLabel);
		GUI.color = Color.gray;
		if (GUILayout.Button("(+)", GUILayout.Width(50)))
		{
			answer.Effects.Add(new Effect());
		}
		EditorGUILayout.EndHorizontal();

		EditorGUILayout.LabelField("Parameter                       -    Operator - Value");
		if (answer.Effects.Count > 0)
		{
			for (var i = 0; i < answer.Effects.Count; i++)
			{
				EditorGUILayout.BeginHorizontal();
				GUI.color = new Color(0.9f, 0.83f, 0.5f);
				var effect = answer.Effects[i];

				//Select Parameter
				var selection = ParameterSetup.Instance.GetParameterIndexById(effect.parameterID);
				selection = EditorGUILayout.Popup(selection, ParameterSetup.Instance.GetAllParameterNames(), GUILayout.Width(200));
				effect.parameterID = ParameterSetup.Instance.GetParameterIdByIndex(selection);

				EditorGUI.indentLevel -= 2;
				effect.Operator = (Operator) EditorGUILayout.EnumPopup(effect.Operator, GUILayout.Width(60));

				if (ParameterSetup.Instance.GetParameterById(effect.parameterID).Type == ParameterType.Boolean)
				{
					effect.Value = EditorGUILayout.ToggleLeft("value", effect.Value > 0, GUILayout.Width(100)) ? 1 : 0;
				}
				else
				{
					effect.Value = EditorGUILayout.IntField(effect.Value);
				}

				GUI.color = new Color(0.9f, 0.7f, 0.7f);
				if (GUILayout.Button("X", GUILayout.Width(25)))
				{
					answer.Effects.Remove(effect);
				}

				EditorGUILayout.EndHorizontal();
				EditorGUI.indentLevel += 2;
			}
		}
		GUI.color = Color.white;
	}

	private void SelectNextScene(Answer answer, DialogStage stage)
	{
		EditorGUILayout.BeginHorizontal();
		var nextStage =
			(EditorGUILayout.ObjectField("NextScene", DialogStageBehaviour.NextStageBehaviour(answer),
				typeof (DialogStageBehaviour), true) as DialogStageBehaviour);
		if (nextStage != null && nextStage.Stage.Id != stage.Id)
		{
			answer.NextStageId = nextStage.Stage.Id;
		}
		else
		{
			answer.NextStageId = -1;
			if (GUILayout.Button("Create Stage"))
			{
				GameObject go=new GameObject();
				go.transform.position=(target as MonoBehaviour).transform.position+Vector3.right*2;
				DialogStageBehaviour stageBehaviour = go.AddComponent<DialogStageBehaviour>();
				stageBehaviour.RefreshId();
				answer.NextStageId=stageBehaviour.Stage.Id;
			}
		}
		EditorGUILayout.EndHorizontal();
	}
}