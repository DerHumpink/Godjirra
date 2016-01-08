using UnityEngine;
using UnityEngine.UI;

public class AnswerButton : MonoBehaviour
{
	private Answer _answer;

	public void SetAnswerData(Answer answer)
	{
		_answer = answer;
		GetComponentInChildren<Text>().text = answer.Text;
		GetComponent<Button>().onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		GameManager.Instance.OnAnswerSelected(_answer);
	}
}