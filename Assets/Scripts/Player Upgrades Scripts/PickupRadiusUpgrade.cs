using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class PickupRadiusUpgrade : Upgrade
{
    [SerializeField] PlayerPickupScript player;
    private int[] pickupRadiusValues;
    private void Start()
    {

        this.upgradeNames = new[]
        {
            "Improved Pickup Radius",
            "Focus Vision",
            "Big Pockets",
            "Loot Fiend"
        };

        pickupRadiusValues = new int[] { 10, 20, 10, 15 };

        this.upgradeDescriptions = new[]
        {
            "Increase <b>Pickup Radius</b> by <b>10%</b>.",
            "Increase <b>Pickup Radius</b> by <b>20%</b>.",
            "Increase <b>Pickup Radius</b> by <b>10%</b>.",
            "Increase <b>Pickup Radius</b> by <b>15%</b>."
        };
    }
    public override void UpgradeAbility()
    {
        player.SetPlayerPickupRange(pickupRadiusValues[this.currentUpgradeTier]);
    }

}
