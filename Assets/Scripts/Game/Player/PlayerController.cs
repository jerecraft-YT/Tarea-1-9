using System;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    //todo lo que esta alrededor debe recibir daño al hacer click

    //cosas de input
    private GameInputs gameInput;

    //cosas de jugador

    [SerializeField] private int baseDamage = 10;
    [SerializeField] private int live = 100;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float distanceToAttack = 3f;
    [SerializeField] private float intialCooldownAttack = .15f;

    private float cooldownAttack;

    private WeaponData weapon;
    private BaseStats stats;
    private Rigidbody2D rb;

    private Vector2 dirMove = Vector2.zero;

    private void Awake()
    {
        SetInput();

        SetWeapon();

        SetStats();

        SetComponents();
    }

    private void SetComponents()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void SetWeapon()
    {
        weapon = new(baseDamage);
    }

    private void SetStats()
    {
        //se define a la vez live y max live
        stats = new(live);
    }

    private void SetInput()
    {
        gameInput = new();
        gameInput.Enable();

        gameInput.Player.Move.performed += OnMove;
        gameInput.Player.Move.canceled += OnMove;

        gameInput.Player.Attack.performed += OnAttack;
        
    }

    private void OnDisable()
    {
        gameInput.Player.Move.performed -= OnMove;
        gameInput.Player.Move.canceled -= OnMove;

        gameInput.Player.Attack.performed -= OnAttack;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        dirMove = ctx.ReadValue<Vector2>();
    }

    private void OnAttack(InputAction.CallbackContext ctx)
    {
        if (cooldownAttack <= 0)
        {
            cooldownAttack = intialCooldownAttack;
            Attack();
        }
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dirMove * playerSpeed;

        CooldownAttackController();
    }
    private void CooldownAttackController()
    {
        if (cooldownAttack > 0)
        {
            cooldownAttack -= Time.fixedDeltaTime;
        }
    }

    private void Attack()
    {
        GameObject[] Enemies = GameObject.FindGameObjectsWithTag("Enemy");

        foreach(var enemy in Enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, transform.position);

            if (distance < distanceToAttack)
            {
                enemy.GetComponent<Enemy>().TakeDamage(weapon.Damage);
            }
        }
    }
}
