using System;
using UnityEngine;

[Serializable]
public struct UpgradeCell
{
    public UpgradeType UpgradeType;
    public bool IsBlueType;
    public Sprite Sprite;
    public string Name;
    public string Description;
}