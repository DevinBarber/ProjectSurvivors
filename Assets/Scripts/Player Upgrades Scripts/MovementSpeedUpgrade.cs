using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MovementSpeedUpgrade : Upgrade
{
    private PlayerScript player;
    private int[] movementSpeedUpgradeValues;
    private void Start()
    {
        player = GetComponentInParent<PlayerScript>();

        this.upgradeNames = new[]
        {
            "Improved Move Speed",
            "Running Shoes",
            "Quick Feet",
            "Fast as fuck"
        };

        movementSpeedUpgradeValues = new int[] { 5, 10, 15, 10 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Movement Speed</b> by <b>5%</b>.",
            "Increase <b>Movement Speed</b> by <b>10%</b>.",
            "Increase <b>Movement Speed</b> by <b>15%</b>.",
            "Increase <b>Movement Speed</b> by <b>10%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerMovementSpeed(movementSpeedUpgradeValues[this.currentUpgradeTier]);
    }

}
