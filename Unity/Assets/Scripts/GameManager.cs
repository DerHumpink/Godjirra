using System.Linq;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Singleton<GameManager>
{

	[SerializeField] private Text _headerText;
	[SerializeField] private Text _storyText;
	[SerializeField] private VerticalLayoutGroup _answersLayoutGroup;
	[SerializeField] private Transform _answerButtonPrefab;

	public DialogTree MainDialogTree { get; private set; }

	private void Start()
	{
		GetMainTree();
		SetStage(GetStageById(1));
	}

	private void SetStage(DialogStage currentStage)
	{
		_headerText.text = currentStage.Name;
		_storyText.text = currentStage.Text;
		foreach (Answer answer in currentStage.Answers)
		{
			Transform answerButton = Instantiate(_answerButtonPrefab);
			answerButton.parent = _answersLayoutGroup.transform;
			answerButton.GetComponent<AnswerButton>().SetAnswerData(answer);
		}
	}

	private void GetMainTree()
	{
		MainDialogTree = JsonUtility.FromJson<DialogTree>(EditorPrefs.GetString("DialogTreeJSON"));
	}

	private DialogStage GetStageById(int id)
	{
		return MainDialogTree.Stages.First(s => s.Id == id);
	}

	public void OnAnswerSelected(Answer answer)
	{

	}
}