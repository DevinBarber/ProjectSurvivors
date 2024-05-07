using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeNavigationScript : MonoBehaviour
{
    [SerializeField] Button secondDisplayButton;
    [SerializeField] Button chooseButton;
    Navigation customNav = new Navigation();

    private void Start()
    {
        customNav.mode = Navigation.Mode.Explicit;
    }

    public void ChangeNavigation(Button selectedButton)
    {
        customNav.selectOnLeft = selectedButton.GetComponent<Button>();
        customNav.selectOnRight = secondDisplayButton;
        customNav.selectOnDown = chooseButton;

        GetComponent<Button>().navigation = customNav;
    }
}
