using UnityEngine;
using UnityEngine.UI;

public class UpgradePanelUI : MonoBehaviour
{
    [SerializeField] private Image _upgradeImage;
    [SerializeField] private Text _upgradeNameText;
    [SerializeField] private Text _upgradeDescriptionText;
    [SerializeField] private Text _upgradeLevelText;
    [SerializeField] private Text _upgradeCostText;
    [SerializeField] private Image _starImage;
    [SerializeField] private Button _buyUpgradeButton;
    [SerializeField] private Sprite _blueStar;
    [SerializeField] private Sprite _redStar;
    [SerializeField] private Color _upgradeDescriptionTextBlueColor;
    [SerializeField] private Color _upgradeDescriptionTextRedColor;
    
    private UpgradeType _upgradeType;
    private bool _isBlueType;

    private void Awake()
    {
        _buyUpgradeButton.onClick.AddListener(() =>
        {
            UpgradesManager.Instance.TryBuyUpgrade(_upgradeType, _isBlueType, gameObject);
        });
    }

    public void UpdateUpgradePanel(UpgradeCell upgradeCell)
    {
        _upgradeType = upgradeCell.UpgradeType;
        _isBlueType = upgradeCell.IsBlueType;

        int upgradeLevel = Upgrades.Types[_upgradeType] + 1;
        int upgradeCost = (Upgrades.Types[_upgradeType] + 1) * 5;
        
        _upgradeImage.sprite = upgradeCell.Sprite;
        _upgradeNameText.text = upgradeCell.Name;
        _upgradeDescriptionText.text = upgradeCell.Description;
        _upgradeLevelText.text = $"Уровень: {upgradeLevel}";
        _upgradeCostText.text = upgradeCost.ToString();

        if (_isBlueType)
        {
            _starImage.sprite = _blueStar;
            _upgradeDescriptionText.color = _upgradeDescriptionTextBlueColor;
        }
        else
        {
            _starImage.sprite = _redStar;
            _upgradeDescriptionText.color = _upgradeDescriptionTextRedColor;
        }
    }
}