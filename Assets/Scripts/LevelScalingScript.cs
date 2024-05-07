using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class LevelScalingScript : MonoBehaviour
{
    [SerializeField] private AnimationCurve xpIncreasePerlevel;
    [SerializeField] private UpgradeManagerScript upgradeScript;
    public PlayerScript player;
    private float previousLevelsExperience;
    private float nextLevelsExperience;
    public TextMeshProUGUI levelText;
    public ExperienceBarUIScript experienceBar;
    public CurrencyManagerScript currencyManager;

    // Start is called before the first frame update
    void Start()
    {
        UpdateLevel();
    }

    public void AddExperience(float xpAmount)
    {
        player.SetPlayerExperience(player.GetPlayerExperience() + xpAmount);
        CheckForLevelUp();
        UpdateUI();
    }

    private void CheckForLevelUp()
    {
        if (player.GetPlayerExperience() >= nextLevelsExperience)
        {
            upgradeScript.LevelUpUpgrade();
            player.SetPlayerLevel(player.GetPlayerLevel() + 1);
            currencyManager.CalculateAmountPerLevel();
            UpdateLevel();
        }
    }

    private void UpdateLevel()
    {
        previousLevelsExperience = xpIncreasePerlevel.Evaluate(player.GetPlayerLevel());
        nextLevelsExperience = xpIncreasePerlevel.Evaluate(player.GetPlayerLevel() + 1);
        UpdateUI();
    }

    private void UpdateUI()
    {
        float start = player.GetPlayerExperience() - previousLevelsExperience;
        float end = nextLevelsExperience - previousLevelsExperience;
        experienceBar.SetSliderExperience(start);
        experienceBar.SetSliderMaxExperience(end);

        levelText.text = "Level: " + player.GetPlayerLevel().ToString();
    }

}
