using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
	private Answer _answer;

	public void SetAnswerData(Answer answer)
	{
		_answer = answer;
		GetComponentInChildren<Text>().text = answer.Text;
		Button button = GetComponent<Button>();
		button.onClick.AddListener(OnClicked);
		button.interactable = AreConditionsFullfilled(answer.Conditions);
	}

	private bool AreConditionsFullfilled(List<Condition> conditions)
	{
		return conditions.All(condition => condition.IsFullfielled());
	}

	private void OnClicked()
	{
		GameManager.Instance.OnAnswerSelected(_answer);
	}
}