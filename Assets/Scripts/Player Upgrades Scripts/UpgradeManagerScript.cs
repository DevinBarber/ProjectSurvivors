using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class UpgradeManagerScript : MonoBehaviour
{
    public List<UpgradeButton> upgradeButtons;
    public List<DisplayButton> displayButtons;
    public List<Upgrade> upgrades;
    public List<Upgrade> upgradesTaken = new List<Upgrade>();

    [SerializeField] TextMeshProUGUI upgradeNameText;
    [SerializeField] TextMeshProUGUI upgradeDescriptionText;
    [SerializeField] GameObject upgradePanel;

    private int upgradeBias = 20;
    public bool isUpgrading = false;

    public int currentlySubmitted;
    public string lastSelectedName;
    public string lastSelectedDescription;
    public Upgrade lastSelectedUpgrade;
    int activeButtons;

    // Start is called before the first frame update
    void Start()
    {
        upgradePanel.SetActive(false);
        UpdateUpgradeIDList();
    }

    public void UpdateUpgradeIDList()
    {
        for (int i = 0; i < upgrades.Count; i++)
        {
            upgrades[i].id = i;
        }
    }

    public void LevelUpUpgrade()
    {
        isUpgrading = true;
        Time.timeScale = 0;
        PanelSetupAndDisplay();
    }

    public void ChooseUpgrade()
    {
        if(activeButtons > 0)
        {
            if (upgradesTaken.Contains(upgradeButtons[currentlySubmitted].upgrade))
            {
                //Upgrade path is already taken
            } else
            {
                upgradesTaken.Add(upgradeButtons[currentlySubmitted].upgrade);
            }

            upgradeButtons[currentlySubmitted].upgrade.TriggerAbility();

            if (upgradeButtons[currentlySubmitted].upgrade.maxUpgrade)
            {
                upgrades.Remove(upgradeButtons[currentlySubmitted].upgrade);
                upgradesTaken.Remove(upgradeButtons[currentlySubmitted].upgrade);
            }
        }

        upgradePanel.SetActive(false);
        Time.timeScale = 1f;
        isUpgrading = false;
    }

    public void UpdateDisplayButtons(Upgrade upgrade)
    {
        for (int i = 0; i < displayButtons.Count; i++)
        {
            displayButtons[i].SetupDisplay(upgrade, i);
        }
    }

    public void UpdatePanelText(string upgradeName, string upgradeDescription)
    {
        upgradeNameText.text = upgradeName;
        upgradeDescriptionText.text = upgradeDescription;
    }

    public void UpdatePanelTextLastSelected(string upgradeName, string upgradeDescription, Upgrade upgrade)
    {
        lastSelectedName = upgradeName;
        lastSelectedDescription = upgradeDescription;
        lastSelectedUpgrade = upgrade;
    }
    public void UpdatePanelTextLastSelectedDisplay(string upgradeName, string upgradeDescription)
    {
        lastSelectedName = upgradeName;
        lastSelectedDescription = upgradeDescription;
    }

    public void UpdatePanelSubmitted()
    {
        upgradeNameText.text = upgradeButtons[currentlySubmitted].upgradeName;
        upgradeDescriptionText.text = upgradeButtons[currentlySubmitted].upgradeDescription;

        for (int i = 0; i < displayButtons.Count; i++)
        {
            displayButtons[i].SetupDisplay(upgradeButtons[currentlySubmitted].upgrade, i);
        }
    }

    public void UpdatePanelTextSelected()
    {
        //upgradeNameText.text = upgradeButtons[currentlySubmitted].upgradeName;
        //upgradeDescriptionText.text = upgradeButtons[currentlySubmitted].upgradeDescription;
        upgradeNameText.text = lastSelectedName;
        upgradeDescriptionText.text = lastSelectedDescription;

        for (int i = 0; i < displayButtons.Count; i++)
        {
            displayButtons[i].SetupDisplay(lastSelectedUpgrade, i);
        }
    }

    public void PanelSetupAndDisplay()
    {
        upgradePanel.SetActive(true);
        List<Upgrade> upgradesToChoose = new List<Upgrade>();
        List<Upgrade> takenUpgradesToChoose = new List<Upgrade>();
        currentlySubmitted = 0;

        for(int i = 0; i < upgrades.Count(); i++)
        {
            upgradesToChoose.Add(upgrades[i]);
        }

        if(upgradesTaken.Count > 0)
        {
            for (int i = 0; i < upgradesTaken.Count(); i++)
            {
                takenUpgradesToChoose.Add(upgradesTaken[i]);
            }
        }

        if (upgradesToChoose.Count < upgradeButtons.Count)
        {
            activeButtons = upgradesToChoose.Count;
            upgradeButtons[upgradesToChoose.Count].gameObject.SetActive(false);
        } else
        {
            activeButtons = upgradeButtons.Count;
        }

        for (int i = 0; i < activeButtons; i++)
        {
            int temp = 0;
            int bias = Random.Range(1, 101); //number between 1-100

            if(bias <= upgradeBias && takenUpgradesToChoose.Count > 0) //If hit a bias and we have atleast one upgrade 
            {
                int randomTakenUpgrade = Random.Range(0, takenUpgradesToChoose.Count); //Get a random taken upgrade from out list

                if (upgradesToChoose.Contains(takenUpgradesToChoose[randomTakenUpgrade])) //If upgrades to choose from contains the upgrade weve already taken
                {
                    temp = upgradesToChoose.IndexOf(takenUpgradesToChoose[randomTakenUpgrade]); //set temp equal to the index of the taken upgrade
                    takenUpgradesToChoose.Remove(takenUpgradesToChoose[randomTakenUpgrade]); //Remove upgrade from list so we cant have a repeat or null
                }

            } else
            {
                temp = Random.Range(0, upgradesToChoose.Count);
            }

            upgradeButtons[i].Set(upgradesToChoose[temp], i);

            if (i == 0)
            {
                UpdatePanelText(upgradesToChoose[temp].upgradeNames[upgradesToChoose[temp].currentUpgradeTier], upgradesToChoose[temp].upgradeDescriptions[upgradesToChoose[temp].currentUpgradeTier]);
                
                for(int j = 0; j < displayButtons.Count; j++)
                {
                    displayButtons[j].SetupDisplay(upgradesToChoose[temp], j);
                }
            }

            upgradesToChoose.RemoveAt(temp);

        }

    }
    
}
