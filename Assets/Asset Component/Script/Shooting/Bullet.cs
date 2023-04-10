using System;
using Unity.VisualScripting;
using UnityEngine;
using EdgeMultiplay;

public class Bullet : MonoBehaviour
{
    private GameObject playerObject;
    private TankController tankController;
    private ShootController shootController;
    private GameManager gameManager;
    // private ObservableView view;
    
    private void Awake()
    {
        gameManager = GameObject.FindWithTag("GameManager").GetComponent<GameManager>();
        playerObject = GameObject.FindWithTag("Player").gameObject;
        tankController = playerObject.GetComponent<TankController>();
        shootController = playerObject.GetComponent<ShootController>();
    }

    // private void Start()
    // {
    //     view = GetComponent<ObservableView>();
    //     if(view == null)
    //     {
    //         throw new System.Exception("Can't find ObservableView component on the Bullet.");
    //     }
    // }

    private void OnTriggerEnter2D(Collider2D collider)
    {
        switch (collider.gameObject.tag)
        {
            case "Player":
                EdgeManager.MessageSender.BroadcastMessage(new GamePlayEvent
                {
                    eventName = "playerHit",
                    integerData = new int[2]
                    { shootController.BulletDamage, tankController.playerIndex }
                });
                gameManager.DecreaseHp(shootController.BulletDamage, tankController.playerIndex);
                Destroy(gameObject);
                break;
            case "Wall":
                Destroy(gameObject);
                break;
            default:
                Debug.Log("Bullet hit something else");
                break;
        }
    }
}