using System;
using UnityEngine;
using UnityEngine.UI;

public class GameUI : MonoBehaviour
{
    [SerializeField] private Text _lvlText;
    [SerializeField] private Text _blueStarsText;
    [SerializeField] private Text _redStarsText;
    [SerializeField] private Text _levelTimerText;
    [SerializeField] private Slider _playerHpSlider;
    
    private void Awake()
    {
        UpdateLevelText();
        UpdateStarsText();
    }

    private void Start()
    {
        UpgradesManager.Instance.OnBuyUpgrade += UpgradesManager_OnBuyUpgrade;
        UpgradesManager.Instance.OnReroll += UpgradesManager_OnReroll;
        PlayerShip.Instance.OnCollectStar += PlayerShip_OnCollectStar;
        PlayerShip.Instance.OnPlayerHealthChange += PlayerShip_OnPlayerHealthChange;
        
        _playerHpSlider.maxValue = PlayerShip.Instance.GetMaxHealth();
        _playerHpSlider.minValue = 0;
        _playerHpSlider.value = _playerHpSlider.maxValue;
    }

    private void Update()
    {
        _levelTimerText.text = GameManager.Instance.GetLevelTimer().ToString("00");
    }

    private void UpgradesManager_OnBuyUpgrade(object sender, EventArgs e)
    {
        UpdateStarsText();
    }
    
    private void UpgradesManager_OnReroll(object sender, EventArgs e)
    {
        UpdateStarsText();
    }
    
    private void PlayerShip_OnCollectStar(object sender, EventArgs e)
    {
        UpdateStarsText();
    }
    
    private void PlayerShip_OnPlayerHealthChange(object sender, PlayerShip.OnPlayerHealthChangeEventArgs e)
    {
        _playerHpSlider.value = e.CurrentHealth;
    }

    private void UpdateStarsText()
    {
        _blueStarsText.text = Stats.BlueStars.ToString("000");
        _redStarsText.text = Stats.RedStars.ToString("000");
    }

    private void UpdateLevelText()
    {
        int level = Stats.CurLevel;
        _lvlText.text =$"Уровень: {level+1}";
    }
}