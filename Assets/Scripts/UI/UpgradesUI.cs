using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradesUI : MonoBehaviour
{
	[SerializeField] private Button _rerollButton;
	[SerializeField] private Button _nextLevelButton;
	[SerializeField] private UpgradePanelUI[] _upgradePanels;

	private void Awake()
	{
		_rerollButton.onClick.AddListener(() =>
		{
			UpgradesManager.Instance.TryReroll();
		});
		
		_nextLevelButton.onClick.AddListener(() =>
		{
			SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
		});
	}
	
	private void Start()
	{
		GameManager.Instance.OnWinLevel += GameManager_OnWinLevel;
		UpgradesManager.Instance.OnReroll += UpgradesManager_OnReroll;
		
		Hide();
	}

	private void UpdateUpgradePanels()
	{
		List<UpgradeCell> cells = UpgradesManager.Instance.GetUpgradeCells(_upgradePanels.Length);
        
		for (int i = 0; i < _upgradePanels.Length; i++)
		{
			_upgradePanels[i].UpdateUpgradePanel(cells[i]);
		}
	}

	private void GameManager_OnWinLevel(object sender, EventArgs e)
	{
		UpdateUpgradePanels();
		Show();
	}
	
	private void UpgradesManager_OnReroll(object sender, EventArgs e)
	{
		UpdateUpgradePanels();
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