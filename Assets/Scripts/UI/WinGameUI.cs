using System;
using UnityEngine;
using UnityEngine.UI;

public class WinGameUI : MonoBehaviour
{
	[SerializeField] private Button _mainMenuButton;
	
	private void Awake()
	{
		_mainMenuButton.onClick.AddListener(() =>
		{
			SceneLoader.LoadScene(SceneLoader.Scene.MainMenuScene);
		});
	}

	private void Start()
	{
		GameManager.Instance.OnWinGame += GameManager_OnWinGame;
		
		Hide();
	}

	private void GameManager_OnWinGame(object sender, EventArgs e)
	{
		Show();
	}

	private void Show()
	{
		gameObject.SetActive(true);
	}

	private void Hide()
	{
		gameObject.SetActive(false);
	}
}