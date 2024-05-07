using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using System.Xml.Serialization;
using Unity.VisualScripting;

public class PlayerMovementScript : MonoBehaviour
{
    //Player movement
    public PlayerInputActions playerControls;
    public Rigidbody2D rb;
    public Slider dashCooldownSlider;
    [SerializeField] PlayerScript player;
    [SerializeField] GameManagerScript gameManager;
    [SerializeField] UpgradeManagerScript upgradeManager;
    [SerializeField] PlayerLookAtMouseScript mousePos;
    Vector2 moveDirection = Vector2.zero;
    Vector2 moveDirectionInverted = Vector2.zero;
    private InputAction move;
    private InputAction moveInverted;
    private InputAction dash;
    public float lastHorizontalVector;
    public float lastVerticalVector;

    //Player Dash
    [SerializeField] float dashSpeed;
    [SerializeField] float dashDuration = 0.25f;
    [SerializeField] float dashCooldown = 3f;
    private float dashCurrentCooldown;
    private bool isDashing;
    private bool canDash = true;

    private bool isFeared;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        isFeared = false;
        move = playerControls.Player.Move;
        move.Enable();
        moveInverted = playerControls.Player.MoveInverted;
        moveInverted.Enable();
        dash = playerControls.Player.Dash;
        dash.Enable();
        dash.performed += Dash;
        InputSystem.EnableDevice(Keyboard.current);
    }

    private void OnDisable()
    {
        moveInverted.Disable();
        move.Disable();
        dash.performed -= Dash;
        dash.Disable();
    }

    private void Start()
    {
        dashCurrentCooldown = dashCooldown;
        dashSpeed = player.GetPlayerMovementSpeed() * 2; //Check
    }

    void Update()
    {
        moveDirection = move.ReadValue<Vector2>();
        moveDirectionInverted = moveInverted.ReadValue<Vector2>();

        if (isDashing)
        {
            return;
        }

        dashCurrentCooldown += Time.deltaTime;
        dashCooldownSlider.value = dashCurrentCooldown / dashCooldown;

        if (dashCooldownSlider.value >= 1f)
        {
            dashCooldownSlider.gameObject.SetActive(false);
        } else
        {
            dashCooldownSlider.gameObject.SetActive(true);
        }

        if (moveDirection.x != 0)
        {
            lastHorizontalVector = moveDirection.x;
        }
        if (moveDirection.y != 0)
        {
            lastVerticalVector = moveDirection.y;
        }

        /*if (Input.GetKeyDown(KeyCode.Space) && canDash && gameManager.isPaused == false && upgradeManager.isUpgrading == false)
        {
            StartCoroutine(PlayerDashMovingRoutine());
        }*/
    }

    void FixedUpdate()
    {
        if (isDashing)
        {
            return;
        }

        if (!isFeared)
        {
            rb.velocity = new Vector2(moveDirection.x * player.GetPlayerMovementSpeed(), moveDirection.y * player.GetPlayerMovementSpeed());
        } else
        {
            rb.velocity = new Vector2(moveDirectionInverted.x * player.GetPlayerMovementSpeed(), moveDirectionInverted.y * player.GetPlayerMovementSpeed());
        }
    }

    /*private IEnumerator PlayerDashLookingAtMouseRoutine()
    {
        canDash = false;
        isDashing = true;
        Vector2 mouseDir = (mousePos.GetMousePos() - transform.position).normalized;
        rb.velocity = new Vector2(mouseDir.x * dashSpeed, mouseDir.y * dashSpeed);
        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }*/

    private void Dash(InputAction.CallbackContext context)
    {
        if (canDash && gameManager.isPaused == false && upgradeManager.isUpgrading == false && rb.velocity != Vector2.zero) 
        {
            StartCoroutine(PlayerDashMovingRoutine());
        } 
    }

    private IEnumerator PlayerDashMovingRoutine()
    {
        canDash = false;
        isDashing = true;
        dashCurrentCooldown = 0;

        if (!isFeared)
        {
            rb.velocity = new Vector2(moveDirection.x * dashSpeed, moveDirection.y * dashSpeed);
        } else
        {
            rb.velocity = new Vector2(moveDirectionInverted.x * dashSpeed, moveDirectionInverted.y * dashSpeed);
        }

        yield return new WaitForSeconds(dashDuration);
        isDashing = false;

        yield return new WaitForSeconds(dashCooldown);
        canDash = true;
    }
    public IEnumerator FearPlayerFromVoidTerror(float fearDuration)
    {
        isFeared = true;
        yield return new WaitForSeconds(fearDuration);
        isFeared = false;
    }

    public Vector2 GetPlayerMoveDirection()
    {
        return moveDirection;
    }
}
