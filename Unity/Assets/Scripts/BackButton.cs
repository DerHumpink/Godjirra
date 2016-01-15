using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class BackButton : MonoBehaviour
{
	private void Start()
	{
		var button = GetComponent<Button>();
		button.onClick.AddListener(OnClicked);
	}

	private void OnClicked()
	{
		GameManager.Instance.LoadPreviousScene();
	}
}