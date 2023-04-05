using System;
using System.Collections;
using System.Collections.Generic;
using EdgeMultiplay;
using UnityEngine;
using UnityEngine.Serialization;

public class TankController : MonoBehaviour
{
    #region Basic Components

    [Header("Basic Components")] 
    [SerializeField] private float tankSpeed;
    [SerializeField] private Vector2 tankMoveInput;

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
        // gameManager = FindObjectOfType<GameManager>();
        // playerManager = FindObjectOfType<PlayerManager>();
        myRb = GetComponent<Rigidbody2D>();
        myAnim = GetComponent<Animator>();
    }
    private void OnDestroy()
    {
        // StopListening();
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
        moveHorizontal = Input.GetAxis("Horizontal");
        moveVertical = Input.GetAxis("Vertical");
        
        tankMoveInput = new Vector2(moveHorizontal, moveVertical);
        tankMoveInput.Normalize();
        
        myRb.velocity = tankMoveInput * tankSpeed;
        PlayerAnimation();
    }
    
    private void PlayerAnimation()
    {
        if (tankMoveInput != Vector2.zero)
        {
            myAnim.SetFloat(HORIZONTAL_MOVE, tankMoveInput.x);
            myAnim.SetFloat(VERTICAL_MOVE, tankMoveInput.y);
            myAnim.SetBool(IS_MOVING, true);
        }
        else
        {
            myAnim.SetBool(IS_MOVING, false);
        }
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
