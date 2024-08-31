using System;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance { get; private set; }

    public event EventHandler OnWinLevel;
    public event EventHandler OnWinGame;
    
    public bool IsBlueTheme { get; private set; } = true;
    public bool IsGamePlaying => _gameState == State.GamePlaying;
    
    [SerializeField] private float _levelDuration;
    [SerializeField] private float _themeDuration;
    [SerializeField] private SpriteRenderer _background;
    [SerializeField] private Sprite _blueThemeSprite;
    [SerializeField] private Sprite _redThemeSprite;
    
    private float _levelTimer;
    private float _themeTimer;
    private State _gameState;
    
    private enum State
    {
        GamePlaying,
        GameOver,
    }

    private void Awake()
    {
        Instance = this;
        
        _levelTimer = _levelDuration;
        _themeTimer = _themeDuration;

        _gameState = State.GamePlaying;
    }

    private void Update()
    {
        switch (_gameState)
        {
            case State.GamePlaying:
                _levelTimer -= Time.deltaTime;
                _themeTimer -= Time.deltaTime;
                
                if (_themeTimer <= 0)
                {
                    _themeTimer = _themeDuration;
                    ChangeTheme();
                }
                
                if (_levelTimer <= 0)
                {
                    Stats.CurLevel++;

                    if (Stats.CurLevel >= 5)
                    {
                        Stats.ResetAll();
                        OnWinGame?.Invoke(this, EventArgs.Empty);
                    }
                    else
                    {
                        OnWinLevel?.Invoke(this, EventArgs.Empty);
                    }

                    _gameState = State.GameOver;
                }
                
                break;
            case State.GameOver:
                break;
        }
    }

    public float GetLevelTimer()
    {
        return _levelTimer;
    }

    private void ChangeTheme()
    {
        IsBlueTheme = !IsBlueTheme;
        _background.sprite = IsBlueTheme ? _blueThemeSprite : _redThemeSprite;
    }
}