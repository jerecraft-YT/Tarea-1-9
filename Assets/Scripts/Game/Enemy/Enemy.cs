using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int baseDamage = 10;
    [SerializeField] private int baseLive = 100;
    [SerializeField] private int maxLive = 100;

    private WeaponData weapon;
    private BaseStats stats;

    private void Awake()
    {
        weapon = new(baseDamage);
    }

    public void TakeDamage(int damage)
    {
        stats.TakeDamage(damage);
    }


}
