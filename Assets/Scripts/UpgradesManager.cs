using System;
using System.Collections.Generic;
using UnityEngine;

public class UpgradesManager : MonoBehaviour
{
    public static UpgradesManager Instance { get; private set; }

    public event EventHandler OnBuyUpgrade;
    public event EventHandler OnReroll;
    
    [SerializeField] private UpgradeCell[] _upgradesCells;

    private void Awake()
    {
        Instance = this;
    }

    public void TryBuyUpgrade(UpgradeType type, bool isBlueType, GameObject button)
    {
        int cost = (Upgrades.Types[type] + 1) * 5;
        
        if (isBlueType)
        {
            if (Stats.BlueStars >= cost)
            {
                Stats.BlueStars -= cost;
                OnBuyUpgrade?.Invoke(this, EventArgs.Empty);
                Upgrades.AddUpgrade(type);
                button.SetActive(false);
            }
        }
        else
        {
            if (Stats.RedStars >= cost)
            {
                Stats.RedStars -= cost;
                OnBuyUpgrade?.Invoke(this, EventArgs.Empty);
                Upgrades.AddUpgrade(type);
                button.SetActive(false);
            }
        }
    }

    public void TryReroll()
    {
        if (Stats.BlueStars >= 1 && Stats.RedStars >= 1)
        {
            Stats.BlueStars -= 1;
            Stats.RedStars -= 1;
            OnReroll?.Invoke(this, EventArgs.Empty);
        }
    }

    public List<UpgradeCell> GetUpgradeCells(int amount)
    {
        List<UpgradeCell> cells = new List<UpgradeCell>(_upgradesCells);
        List<UpgradeCell> result = new List<UpgradeCell>();

        for (int i = 0; i < amount; i++)
        {
            UpgradeCell cell = cells[UnityEngine.Random.Range(0, cells.Count)];
            result.Add(cell);
            cells.Remove(cell);
        }

        return result;
    }
}