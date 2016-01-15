using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;

public class GameManager : Singleton<GameManager>
{
	[SerializeField] private Transform _answerButtonPrefab;
	[SerializeField] private VerticalLayoutGroup _answersLayoutGroup;

	[SerializeField] private Text _headerText;
	[SerializeField] private Text _storyText;
	private DialogStage _currentStage;

	public List<DialogStage> PreviousStages { get; private set; }

	private void Start()
	{
		PreviousStages=new List<DialogStage>();
		SetStage(GetStageById(1));
	}

	private void SetStage(DialogStage newStage)
	{
		_headerText.text = newStage.Name;
		_storyText.text = newStage.Text;

		//Delete all old buttons
		foreach (var button in _answersLayoutGroup.GetComponentsInChildren<AnswerButton>())
		{
			Destroy(button.gameObject);
		}

		foreach (var answer in newStage.Answers)
		{
			var answerButton = Instantiate(_answerButtonPrefab);
			answerButton.SetParent(_answersLayoutGroup.transform);
			answerButton.GetComponent<AnswerButton>().SetAnswerData(answer);
		}
		_currentStage = newStage;
	}

	private DialogStage GetStageById(int id)
	{
		return DialogTreeSetup.Instance.Stages.FirstOrDefault(s => s.Id == id);
	}

	public void OnAnswerSelected(Answer answer)
	{
		PreviousStages.Add(_currentStage);
		SetStage(GetStageById(answer.NextStageId));
	}

	public void LoadPreviousScene()
	{
		if(PreviousStages.Count<=0)return;
		PreviousStages.Remove(_currentStage);
		SetStage(PreviousStages.Last());
	}
}
