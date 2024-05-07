using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Upgrade : MonoBehaviour
{
    public int id;
    public int currentUpgradeTier = 0;
    public bool maxUpgrade = false;
    public string[] upgradeNames;
    public string[] upgradeDescriptions;
    public Sprite[] upgradeSprites = new Sprite[4];

    public abstract void UpgradeAbility();

    public void TriggerAbility()
    {
        switch(currentUpgradeTier)
        {
            case 0:
                UpgradeAbility();
                currentUpgradeTier++;
                break;
            case 1:
                UpgradeAbility();
                currentUpgradeTier++;
                break;
            case 2: 
                UpgradeAbility();
                currentUpgradeTier++;
                break;
            case 3: 
                UpgradeAbility(); 
                maxUpgrade = true;
                break;
            default:
                break;
        }
    }
}
