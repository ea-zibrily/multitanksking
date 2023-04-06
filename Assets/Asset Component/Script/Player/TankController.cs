using System;
using System.Collections;
using System.Collections.Generic;
using EdgeMultiplay;
using UnityEngine;
using CodeMonkey.Utils;

public class TankController : MonoBehaviour
{
    #region Basic Components

    [Header("Basic Components")] 
    [SerializeField] private float tankSpeed;
    [SerializeField] private Vector2 tankMoveDirection;
    [SerializeField] private Vector2 tankAimDirection;

    #endregion

    #region Animation Tag

    private const string HORIZONTAL_MOVE = "Horizontal";
    private const string VERTICAL_MOVE = "Vertical";
    private const string IS_MOVING = "isMoving";

    #endregion

    #region Reference
    
    [Header("Reference")]
    private GameManager gameManager;
    private PlayerManager playerManager;
    private Rigidbody2D myRb;
    private Animator myAnim;

    #endregion
    
    private void OnEnable()
    {
        // ListenToMessages();
        gameManager = FindObjectOfType<GameManager>();
        playerManager = FindObjectOfType<PlayerManager>();
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        // StopListening();
    }

    private void Update()
    {
        // if (isLocalPlayer && ActivePlayer)
        // {
        //     PlayerAim();
        // }
        PlayerAim();
    }

    private void FixedUpdate()
    {
        // if (isLocalPlayer && ActivePlayer)
        // {
        //     PlayerMovement();
        // }
        
        PlayerMovement();
    }

    #region Controller Methods

    private void PlayerMovement()
    {
        float moveHorizontal, moveVertical;
        moveHorizontal = Input.GetAxisRaw("Horizontal");
        moveVertical = Input.GetAxisRaw("Vertical");
        
        tankMoveDirection = new Vector2(moveHorizontal, moveVertical);
        tankMoveDirection.Normalize();
        
        myRb.velocity = tankMoveDirection * tankSpeed;
    }

    private void PlayerAim()
    {
        Vector3 mousePosition = UtilsClass.GetMouseWorldPosition();
        Vector3 aimDirection = (mousePosition - transform.position);
        aimDirection.Normalize();
        float angle = Mathf.Atan2(aimDirection.y, aimDirection.x) * Mathf.Rad2Deg;
        
        tankAimDirection = new Vector2(aimDirection.x, aimDirection.y);
        myRb.rotation = angle;
    }

    #endregion
    
    // #region NetworkedPlayer Callbacks
    //
    // public override void OnMessageReceived(GamePlayEvent gameEvent)
    // {
    //     switch (gameEvent.eventName)
    //     {
    //         case "restart":
    //             gameManager.RestartGame();
    //             break;
    //         case "score":
    //             gameManager.UpdateScore(gameEvent.integerData[0], gameEvent.integerData[1]);
    //             break;
    //     }
    // }
    //
    // #endregion
}
