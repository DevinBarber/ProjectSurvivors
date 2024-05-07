using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireRateUpgrade : Upgrade
{
    private PlayerScript player;
    private int[] fireRateUpgradeValues;
    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();

        this.upgradeNames = new[]
        {
            "Improved Fire Rate",
            "More Fire Rate",
            "Rapid Fire",
            "Spell Maniac"
        };

        fireRateUpgradeValues = new int[] { 10, 15, 10, 20 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Fire Rate</b> by <b>10%</b>.",
            "Increase <b>Fire Rate</b> by <b>15%</b>.",
            "Increase <b>Fire Rate</b> by <b>10%</b>.",
            "Increase <b>Fire Rate</b> by <b>20%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerFireRate(fireRateUpgradeValues[this.currentUpgradeTier]);
    }

}
