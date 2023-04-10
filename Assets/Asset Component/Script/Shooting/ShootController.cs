using System;
using UnityEngine;

public class ShootController : MonoBehaviour
{
    #region Basic Components

    [Header("Basic Components")]
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameObject bulletPrefab;
    [field: SerializeField] public int BulletDamage { get; private set; }
    [SerializeField] private Transform[] firePoint;

    #endregion

    #region Reference
    
    private TankController tankController;

    #endregion

    private void Awake()
    {
        tankController = GetComponent<TankController>();
    }

    private void Update()
    {
        if (tankController.isLocalPlayer && tankController.ActivePlayer)
        {
            if (Input.GetMouseButtonDown(0))
            {
                Shoot();
            }
        }
    }

    private void Shoot()
    {
        if (firePoint.Length <= 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[0].position, firePoint[0].rotation);
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            rb.AddForce(firePoint[0].right * bulletSpeed, ForceMode2D.Impulse);
            
            Destroy(bullet, bulletLifeTime);
        }
        
        if (firePoint.Length > 1)
        {
            GameObject bullet = Instantiate(bulletPrefab, firePoint[0].position, firePoint[0].rotation);
            GameObject bullet2 = Instantiate(bulletPrefab, firePoint[1].position, firePoint[1].rotation);
        
            Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
            Rigidbody2D rb2 = bullet2.GetComponent<Rigidbody2D>();
        
            rb.AddForce(firePoint[0].right * bulletSpeed, ForceMode2D.Impulse);
            rb2.AddForce(firePoint[1].right * bulletSpeed, ForceMode2D.Impulse);
            
            Destroy(bullet, bulletLifeTime);
            Destroy(bullet2, bulletLifeTime);
        }
    }
}