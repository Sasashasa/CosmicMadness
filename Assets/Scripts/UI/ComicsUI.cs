using UnityEngine;
using UnityEngine.UI;

public class ComicsUI : MonoBehaviour
{
    [SerializeField] private Image _comicImage;
    [SerializeField] private Button _nextButton;
    [SerializeField] private Sprite[] _comicsSprites;
    
    private int _comicId;

    private void Awake()
    {
        _comicImage.sprite = _comicsSprites[0];
        
        _nextButton.onClick.AddListener(() =>
        {
            _comicId++;

            if (_comicId >= _comicsSprites.Length)
            {
                SceneLoader.LoadScene(SceneLoader.Scene.GameScene);
            }
            else
            {
                _comicImage.sprite = _comicsSprites[_comicId];
            }
        });
        
        Hide();
    }

    public void Show()
    {
        gameObject.SetActive(true);
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }
}