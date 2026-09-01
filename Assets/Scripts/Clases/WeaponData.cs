using System;

public class WeaponData
{
    private int damage = 10;

    public WeaponData()
    {
    }

    public WeaponData(int damage)
    {
        this.damage = damage;
    }

    public int Damage => damage;
}
