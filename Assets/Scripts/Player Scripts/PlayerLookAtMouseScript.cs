using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerLookAtMouseScript : MonoBehaviour
{
    public bool isGamepad;
    [SerializeField] GameObject gamepadCrosshair;
    public PlayerInputActions playerControls;
    private InputAction look;

    private Transform m_transform;
    public Camera mainCamera;
    private Vector3 mousePos;
    private Vector2 lookDir;
    //private float rotationSpeed = 750f;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        look = playerControls.Player.Look;
    }

    private void OnEnable()
    {
        look.Enable();
    }
    private void OnDisable()
    {
        look.Disable();
    }

    // Start is called before the first frame update
    void Start()
    {
        m_transform = this.transform;

        if (isGamepad)
        {
            Cursor.visible = false;
            gamepadCrosshair.SetActive(true);
        }
        else
        {
            Cursor.visible = true;
            gamepadCrosshair.SetActive(false);
        }
    }

    private void Update()
    {
        lookDir = look.ReadValue<Vector2>();
        mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);

        gamepadCrosshair.transform.rotation = Quaternion.Euler(0, 0, 0);

        if (isGamepad)
        {
            LookDirection();
        } else
        {
            MouseDirection();
        }
    }

    private void MouseDirection()
    {
        Vector3 aimDirection = mousePos - m_transform.position;
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        m_transform.rotation = Quaternion.Euler(0, 0, angle);
    }

    private void LookDirection()
    {
        if (isGamepad)
        {
            if (Mathf.Abs(lookDir.x) > 0.1f || Mathf.Abs(lookDir.y) > 0.1f)
            {
                float angle = Mathf.Atan2(lookDir.y, lookDir.x) * Mathf.Rad2Deg;
                Quaternion targetRotation = Quaternion.Euler(0, 0, angle);
                //m_transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
                m_transform.rotation = targetRotation;
            }
        } 
    }

    public void OnDeviceChange(PlayerInput playerInput)
    {
        isGamepad = playerInput.currentControlScheme.Equals("Gamepad") ? true : false;

        if (isGamepad)
        {
            gamepadCrosshair.SetActive(true);
            Cursor.visible = false;
        } else
        {
            gamepadCrosshair.SetActive(false);
            Cursor.visible = true;
        }
    }

    public Vector2 GetMouseDirection()
    {
        Vector2 dir = (Vector2)(mousePos - m_transform.position);
        dir.Normalize();
        return dir;
    }
}
