using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class DisplayButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, ISelectHandler
{
    [SerializeField] UpgradeManagerScript upgradeManager;
    [SerializeField] Image upgradeSprite;
    private string upgradeName;
    private string upgradeDescription;

    public void SetupDisplay(Upgrade currentlySubmitted, int i)
    {
        upgradeName = currentlySubmitted.upgradeNames[i];
        upgradeDescription = currentlySubmitted.upgradeDescriptions[i];
        upgradeSprite.sprite = currentlySubmitted.upgradeSprites[i];
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelText(upgradeName, upgradeDescription);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelTextSelected();
    }

    public void OnSelect(BaseEventData eventData)
    {
        upgradeManager.UpdatePanelText(upgradeName, upgradeDescription);
        upgradeManager.UpdatePanelTextLastSelectedDisplay(upgradeName, upgradeDescription);
    }
}
