using UnityEngine;
using UnityEngine.UI;

public class MainMenuUI : MonoBehaviour
{
	[SerializeField] private Button _playButton;
	[SerializeField] private Button _exitButton;
	[SerializeField] private ComicsUI _comicsUI;

	private void Awake()
	{
		_playButton.onClick.AddListener(() =>
		{
			_comicsUI.Show();
			Hide();
		});
		
		_exitButton.onClick.AddListener(Application.Quit);
		
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