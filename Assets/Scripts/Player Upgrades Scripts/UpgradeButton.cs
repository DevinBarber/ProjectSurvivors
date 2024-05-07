using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class UpgradeButton : MonoBehaviour, IPointerEnterHandler, IPointerClickHandler, IPointerExitHandler, ISelectHandler, ISubmitHandler
{
    [SerializeField] Image upgradeSprite;
    [SerializeField] UpgradeManagerScript upgradeManager;
    [SerializeField] UpgradeNavigationScript upgradeNavigation;
    public Upgrade upgrade;
    public string upgradeName;
    public string upgradeDescription;
    private int btnNumber;

    public void Set(Upgrade _upgrade, int i)
    {
        upgrade = _upgrade;
        upgradeSprite.sprite = upgrade.upgradeSprites[upgrade.currentUpgradeTier];
        upgradeName = upgrade.upgradeNames[upgrade.currentUpgradeTier];
        upgradeDescription = upgrade.upgradeDescriptions[upgrade.currentUpgradeTier];
        btnNumber = i;

        if(btnNumber == 0)
        {
            this.GetComponent<Button>().Select();
            upgradeManager.currentlySubmitted = btnNumber;
        }
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelText(upgradeName, upgradeDescription);
        upgradeManager.UpdateDisplayButtons(upgrade);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelTextSelected();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        upgradeManager.currentlySubmitted = btnNumber;
    }

    public void OnSelect(BaseEventData eventData)
    {
        upgradeNavigation.ChangeNavigation(this.GetComponent<Button>());
        upgradeManager.UpdatePanelText(upgradeName, upgradeDescription);
        upgradeManager.UpdatePanelTextLastSelected(upgradeName, upgradeDescription, upgrade);
        upgradeManager.UpdateDisplayButtons(upgrade);
    }

    public void OnSubmit(BaseEventData eventData)
    {
        upgradeManager.currentlySubmitted = btnNumber;
    }
}
