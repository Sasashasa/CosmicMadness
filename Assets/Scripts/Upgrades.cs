using System.Collections.Generic;

public static class Upgrades
{
    public static Dictionary<UpgradeType, int> Types = new()
    {
        { UpgradeType.IncreaseDamage, 0 },
        { UpgradeType.DecreaseReloadTime, 0 },
        { UpgradeType.IncreaseMovementSpeed, 0 },
        { UpgradeType.IncreaseBulletSpeed, 0 },
        { UpgradeType.IncreaseHeroHealth, 0 },
        { UpgradeType.CriticalDamageChance, 0 },
        { UpgradeType.ShotsAmount, 0 },
        { UpgradeType.IncreaseShieldHealth, 0 },
    };

    public static float IncreaseDamageUpgrade => Types[UpgradeType.IncreaseDamage];
    public static float DecreaseReloadTimeUpgrade => Types[UpgradeType.DecreaseReloadTime] * 0.05f;
    public static float IncreaseMovementSpeedUpgrade => 1 + Types[UpgradeType.IncreaseMovementSpeed] * 0.05f;
    public static float IncreaseBulletSpeedUpgrade => 1 + Types[UpgradeType.IncreaseBulletSpeed] * 0.05f;
    public static int IncreaseHeroHealthUpgrade => Types[UpgradeType.IncreaseHeroHealth] * 10;
    public static float CriticalDamageChance => Types[UpgradeType.CriticalDamageChance] * 3;
    public static int ShotsAmount => Types[UpgradeType.ShotsAmount];
    public static int IncreaseShieldHealthUpgrade => Types[UpgradeType.IncreaseShieldHealth] * 5;


    public static void AddUpgrade(UpgradeType type)
    {
        Types[type] += 1;
    }

    public static void ResetAll()
    {
        Types = new Dictionary<UpgradeType, int>
        {
            { UpgradeType.IncreaseDamage, 0 },
            { UpgradeType.DecreaseReloadTime, 0 },
            { UpgradeType.IncreaseMovementSpeed, 0 },
            { UpgradeType.IncreaseBulletSpeed, 0 },
            { UpgradeType.IncreaseHeroHealth, 0 },
            { UpgradeType.CriticalDamageChance, 0 },
            { UpgradeType.ShotsAmount, 0 },
            { UpgradeType.IncreaseShieldHealth, 0 },
        };
    }
}