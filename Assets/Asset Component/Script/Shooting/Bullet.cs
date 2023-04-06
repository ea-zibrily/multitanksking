using System;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private GameObject playerObject;
    private PlayerManagg playerManager;
    private ShootController shootController;

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player").gameObject;
        playerManager = playerObject.GetComponent<PlayerManagg>();
        shootController = playerObject.GetComponent<ShootController>();
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (col.CompareTag("Player"))
        {
            playerManager.DecreaseHp(shootController.BulletDamage);
            Destroy(gameObject);
        }
    }
}