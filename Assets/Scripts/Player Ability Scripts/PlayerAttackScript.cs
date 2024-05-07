using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class PlayerAttackScript : MonoBehaviour
{
    [SerializeField] GameObject fireballPrefab;
    [SerializeField] PlayerLookAtMouseScript mouseScript;
    [SerializeField] Transform attackPoint;
    [SerializeField] GameManagerScript gameManager;

    private PlayerScript player;

    public PlayerInputActions playerControls;
    private InputAction attack;

    private bool isAutoAttacking;
    private bool isFeared;
    private float isTryingToAttack;
    private float fearAngle = 20f;

    private void Awake()
    {
        playerControls = new PlayerInputActions();
        attack = playerControls.Player.Attack;
        player = GetComponent<PlayerScript>();
    }

    private void OnEnable()
    {
        isFeared = false;
        isAutoAttacking = false;
        attack.Enable();
    }

    private void OnDisable()
    {
        attack.Disable();
    }

    private void Update()
    {
        isTryingToAttack = attack.ReadValue<float>();

        if (isTryingToAttack > 0 && !isAutoAttacking && gameManager.isPaused == false)
        {
            StartCoroutine(AttackRoutine());
        }
    }

    private IEnumerator AttackRoutine()
    {
        isAutoAttacking = true;

        if(isFeared)
        {
            float fearCone = Random.Range(-fearAngle, fearAngle);
            Quaternion quaternion = Quaternion.Euler(0, 0, fearCone);
            Vector2 fearShootDirection = quaternion * attackPoint.right;
            GameObject newFireBall = Instantiate(fireballPrefab, attackPoint.position, Quaternion.identity);
            Rigidbody2D rb = newFireBall.GetComponent<Rigidbody2D>();
            rb.AddForce(fearShootDirection * player.GetPlayerProjectileSpeed(), ForceMode2D.Impulse);
        } else
        {
            GameObject newFireBall = Instantiate(fireballPrefab, attackPoint.position, Quaternion.identity);
            Rigidbody2D rb = newFireBall.GetComponent<Rigidbody2D>();
            rb.AddForce(attackPoint.right * player.GetPlayerProjectileSpeed(), ForceMode2D.Impulse);
        }

        yield return new WaitForSeconds(player.GetPlayerFireRate());
        isAutoAttacking = false;
    }

    public IEnumerator FearPlayerFromVoidTerrorAA(float fearDuration)
    {
        isFeared = true;
        yield return new WaitForSeconds(fearDuration);
        isFeared = false;
    }
}
