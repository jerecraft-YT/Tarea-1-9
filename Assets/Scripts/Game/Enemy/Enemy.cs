using MoreMountains.Feedbacks;
using System;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    [SerializeField] private int live = 100;
    private BaseStats stats;
    [SerializeField] private MMF_Player mmf_Player;

    private void Awake()
    {
        stats = new(live);
    }

    public void TakeDamage(int damage)
    {
        live -= damage;

        mmf_Player.PlayFeedbacks();

        CheckLive();
    }
    
    private void CheckLive()
    {
        if (live <= 0)
        {
            Destroy(gameObject);
            Debug.Log("mori");
        }
    }
}
