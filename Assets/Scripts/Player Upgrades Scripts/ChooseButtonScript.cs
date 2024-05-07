using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class ChooseButtonScript : MonoBehaviour, ISelectHandler, IPointerEnterHandler, IPointerExitHandler
{
    [SerializeField] UpgradeManagerScript upgradeManager;
    public void OnPointerEnter(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelSubmitted();
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        upgradeManager.UpdatePanelTextSelected();
    }

    public void OnSelect(BaseEventData eventData)
    {
        upgradeManager.UpdatePanelSubmitted();
    }
}
