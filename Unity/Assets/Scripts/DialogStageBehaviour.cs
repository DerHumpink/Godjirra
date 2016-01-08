using System.Linq;
using UnityEditor;
using UnityEngine;

public class DialogStageBehaviour : MonoBehaviour
{
	public static DialogStageBehaviour[] AllBehaviours
	{
		get { return FindObjectsOfType<DialogStageBehaviour>(); }
	}

	public static DialogStageBehaviour NextStageBehaviour(Answer answer)
	{
		return AllBehaviours.FirstOrDefault(b => b.Stage.Id == answer.NextStageId);
	}

	[SerializeField] public DialogStage Stage=new DialogStage();

	public DialogStageBehaviour()
	{
		Stage.Id = -1;
	}

	public void RefreshId()
	{
		if (Stage.Id < 0)
		{
			Stage.Id = IdManager.GetSceneID();
		}
		gameObject.name = Stage.Name;
	}

	public void OnDrawGizmos()
	{
		RefreshId();

		Gizmos.color = Color.white;
		Gizmos.DrawCube(transform.position + Vector3.one*0.5f, Vector3.one);
		Gizmos.color = Color.black;
		Gizmos.DrawWireCube(transform.position + Vector3.one*0.5f, Vector3.one);

		Vector3[] connections = GetConnections();
		for (var i = 0; i < connections.Length; i++)
		{
			var connection = connections[i];
			Vector3 startPoint = transform.position + Vector3.right;

			startPoint.y+=0.25f+((float)i/connections.Length);

			Handles.color = Color.red;
			Handles.DrawLine(startPoint, connection);
		}


		var style = new GUIStyle(EditorStyles.boldLabel);
		style.fontSize = (int) (50/Camera.current.orthographicSize);

		Handles.Label(transform.position, Stage.Name, style);
	}

	private Vector3[] GetConnections()
	{
		if (Stage.Answers.Count > 0)
		{
			return Stage.Answers.Where(a => a.NextStageId > 0).
				Select(s => NextStageBehaviour(s).transform.position).
				OrderBy(v => v.y).ToArray();
		}
		else
		{
			return new Vector3[0];
		}
	}
}
