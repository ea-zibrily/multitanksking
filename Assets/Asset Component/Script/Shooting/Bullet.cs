using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    [SerializeField] private PlayerManagg playerManager;
    [SerializeField] private float bulletDamage;

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerManager.DecreaseHp(bulletDamage);
        }
    }
}