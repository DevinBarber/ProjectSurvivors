using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CritStrikeDamageUpgrade : Upgrade
{
    private PlayerScript player;
    private int[] critDamageUpgradeValues;
    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();

        this.upgradeNames = new[]
        {
            "Improved Critical Strike Damage",
            "Heavy Strikes",
            "More Critical Strike Damage",
            "Deadly Strikes"
        };

        critDamageUpgradeValues = new int[] { 10, 15, 15, 10 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Critical Strike Damage</b> by <b>10%</b>.",
            "Increase <b>Critical Strike Damage</b> by <b>15%</b>.",
            "Increase <b>Critical Strike Damage</b> by <b>15%</b>.",
            "Increase <b>Critical Strike Damage</b> by <b>10%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerCritDamageMultiplayer(critDamageUpgradeValues[this.currentUpgradeTier]);
    }
}
