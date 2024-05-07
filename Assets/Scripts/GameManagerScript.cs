using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.EventSystems;

public class GameManagerScript : MonoBehaviour
{
    [SerializeField] private GameObject gameUI;
    [SerializeField] private GameObject gameCompletedUI;
    [SerializeField] private GameObject gamePausedUI;
    [SerializeField] private Texture2D cursorTexture;
    [SerializeField] private PlayerMovementScript playerMovementScript;
    [SerializeField] PlayerPickupScript playerPickupScript;

    [SerializeField] private GameObject pauseMenuFirst;

    public PlayerInputActions playerControls;
    private InputAction pause;

    public List<TextMeshProUGUI> statText;
    public UpgradeManagerScript upgradeScript;
    public CurrencyManagerScript currencyManagerScript;
    public PlayerScript player;
    private Vector2 cursorHotspot;
    public bool gameOver;
    public bool isPaused;

    private void Awake()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.Confined;
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        pause = playerControls.Player.Pause;
        pause.Enable();
        pause.performed += PauseGame;
    }

    private void OnDisable()
    {
        pause.performed -= PauseGame;
        pause.Disable();
    }

    private void Start()
    {
        cursorHotspot = new Vector2(cursorTexture.width/2, cursorTexture.height/2);
        Cursor.SetCursor(cursorTexture,cursorHotspot,CursorMode.Auto);
        isPaused = false;
        gamePausedUI.SetActive(false);
        gameOver = false;
    }

    // Update is called once per frame
    void Update()
    {

        /*if(Input.GetKeyDown(KeyCode.Escape) && !gameOver && upgradeScript.isUpgrading == false)
        {
            PauseGame();
        }*/
    }
    
    public void GameOver()
    {
        gameOver = true;
        isPaused = true;
        Time.timeScale = 0;
        currencyManagerScript.AddTotalCurrencyToPlayer();
        PlayerDataScript.totalPlayerCurrency += player.GetPlayerTotalCurrency();
        gameUI.SetActive(false);
        gameCompletedUI.SetActive(true);
    }

    public void PauseGame(InputAction.CallbackContext context)
    {
        if (!gameOver && upgradeScript.isUpgrading == false)
        {
            if (!isPaused)
            {
                Time.timeScale = 0;
                isPaused = true;
                DisplayStatsDuringPause();
                gamePausedUI.SetActive(true);
                EventSystem.current.SetSelectedGameObject(pauseMenuFirst);
            }

             else if (isPaused)
            {
                Time.timeScale = 1;
                gamePausedUI.SetActive(false);
                isPaused = false;
                EventSystem.current.SetSelectedGameObject(null);
            }
        }
    }

    public void UnpauseGame()
    {
        Time.timeScale = 1;
        isPaused = false;
        gamePausedUI.SetActive(false);
        EventSystem.current.SetSelectedGameObject(null);
    }

    private void DisplayStatsDuringPause()
    {
        for(int i = 0; i < statText.Count; i++)
        {
            if(i == 0)
            {
                statText[i].text = "Max Health: " + player.GetPlayerMaxHealth().ToString();
            }
            if(i == 1)
            {
                statText[i].text = "Ability Power: " + player.GetPlayerAbilityPower().ToString();
            }
            if (i == 2)
            {
                statText[i].text = "Fire Rate: " + player.GetPlayerFireRate().ToString();
            }
            if (i == 3)
            {
                statText[i].text = "Movement Speed: " + player.GetPlayerMovementSpeed().ToString();
            }
            if (i == 4)
            {
                statText[i].text = "Critical Strike Chance: " + player.GetPlayerCriticalStrikeChance().ToString();
            }
            if (i == 5)
            {
                statText[i].text = "Critical Strike Damage Multiplier: " + player.GetPlayerCritDamageMultiplayer().ToString();
            }
            if (i == 6)
            {
                statText[i].text = "Pickup Radius: " + playerPickupScript.GetPlayerPickupRange().ToString(); 
            }
            if (i == 7)
            {
                statText[i].text = "Projectile Speed: " + player.GetPlayerProjectileSpeed().ToString();
            }
        }
    }
}
