using UnityEngine;

public class ShootController : MonoBehaviour
{
    #region Basic Components

    [Header("Basic Components")] 
    [SerializeField] private float bulletSpeed;
    [SerializeField] private float bulletLifeTime;
    [SerializeField] private GameObject bulletPrefab;
    
    #endregion

    #region Reference

    [Header("Reference")]
    private GameManager gameManager;
    private Transform firePoint;

    #endregion

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
        firePoint = transform.Find("FirePoint");
    }

    private void Update()
    {
        // if (gameManager.ActivePlayer)
        // {
        //     if (Input.GetMouseButtonDown(0))
        //     {
        //         Shoot();
        //     }
        // }
        
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Shoot();
        }
    }

    private void Shoot()
    {
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
        Rigidbody2D rb = bullet.GetComponent<Rigidbody2D>();
        rb.AddForce(firePoint.right * bulletSpeed, ForceMode2D.Impulse);
        
        Destroy(bullet, bulletLifeTime);
    }
}