using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class AbilityPowerUpgrade : Upgrade
{
    private PlayerScript player;
    private int[] abilityPowerUpgradeValues;
    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();

        this.upgradeNames = new[]
        {
            "Improved Ability Power",
            "More Ability Power",
            "Big Dam AP",
            "Ability Master"
        };

        abilityPowerUpgradeValues = new int[] { 15, 10, 15, 20 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Ability Power</b> by <b>15%</b>.",
            "Increase <b>Ability Power</b> by <b>10%</b>.",
            "Increase <b>Ability Power</b> by <b>15%</b>.",
            "Increase <b>Ability Power</b> by <b>20%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerAbilityPower(abilityPowerUpgradeValues[this.currentUpgradeTier]);
    }
}
