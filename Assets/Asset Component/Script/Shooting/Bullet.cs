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
    private ObservableView view;

    private void OnEnable()
    {
        gameManager = FindObjectOfType<GameManager>();
    }

    private void Awake()
    {
        playerObject = GameObject.FindWithTag("Player").gameObject;
        shootController = playerObject.GetComponent<ShootController>();
    }

    private void Start()
    {
        view = GetComponent<ObservableView>();
        if(view == null)
        {
            throw new System.Exception("Can't find ObservableView component on the Bullet.");
        }
    }

    private void OnTriggerEnter2D(Collider2D col)
    {
        if (!view.OwnerIsLocalPlayer())
        {
            switch (col.gameObject.tag)
            {
                case "Player":
                    EdgeManager.MessageSender.BroadcastMessage(new GamePlayEvent
                    {
                        eventName = "PlayerDamaged"
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
}