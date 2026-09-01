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
    [SerializeField] private int baseLive = 100;
    [SerializeField] private int maxLive = 100;

    [SerializeField] private float playerSpeed = 5f;
    [SerializeField] private float distanceToAttack = 3f;

    private WeaponData weapon;
    private BaseStats stats;
    private Rigidbody2D rb;

    private Vector2 dirMove = Vector2.zero;

    private void Awake()
    {
        SetInput();

        weapon = new(baseDamage);

        rb = GetComponent<Rigidbody2D>();
    }

    private void SetInput()
    {
        gameInput = new();
        gameInput.Enable();

        gameInput.Player.Move.performed += OnMove;
        gameInput.Player.Move.canceled += OnMove;
    }

    private void OnDisable()
    {
        gameInput.Player.Move.performed -= OnMove;
        gameInput.Player.Move.canceled -= OnMove;
    }

    private void OnMove(InputAction.CallbackContext ctx)
    {
        dirMove = ctx.ReadValue<Vector2>();
    }

    private void FixedUpdate()
    {
        rb.linearVelocity = dirMove * playerSpeed;
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
