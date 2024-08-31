using System;
using UnityEngine;
using UnityEngine.UI;

public class LoseGameUI : MonoBehaviour
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
		PlayerShip.Instance.OnLoseGame += PlayerShip_OnLoseGame;
		
		Hide();
	}

	private void PlayerShip_OnLoseGame(object sender, EventArgs e)
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