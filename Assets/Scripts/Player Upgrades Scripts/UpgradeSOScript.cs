using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

[CreateAssetMenu(fileName = "New Upgrade", menuName = "ScriptableObject/New Upgrade")]
public class UpgradeSOScript : ScriptableObject
{
    [SerializeField] string upgradeName;
    [SerializeField] string upgradeToolTip;
    [SerializeField] Sprite upgradeSpriteImage;

    #region "Getters"
    public string GetUpgradeName()
    {
        return upgradeName;
    }
    public string GetUpgradeToolTip()
    {
        return upgradeToolTip;
    }
    public Sprite GetUpgradeSpriteImage()
    {
        return upgradeSpriteImage;
    }

    #endregion
}
