using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritStrikeChanceUpgrade : Upgrade
{
    private PlayerScript player;
    private int[] critChanceUpgradeValues;
    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();

        this.upgradeNames = new[]
        {
            "Improved Critical Strike Chance",
            "Improved Accuracy",
            "Deadly Focus",
            "Clear Mind"
        };

        critChanceUpgradeValues = new int[] { 5, 10, 15, 20 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Critical Strike Chance</b> by <b>5%</b>.",
            "Increase <b>Critical Strike Chance</b> by <b>10%</b>.",
            "Increase <b>Critical Strike Chance</b> by <b>15%</b>.",
            "Increase <b>Critical Strike Chance</b> by <b>20%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerCriticalStrikeChance(critChanceUpgradeValues[this.currentUpgradeTier]);
    }
}
