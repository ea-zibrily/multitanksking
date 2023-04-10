using System;
using UnityEngine;
using EdgeMultiplay;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.Serialization;
using UnityEngine.UIElements;
using Image = UnityEngine.UI.Image;

[RequireComponent(typeof(EdgeManager))]
public class GameManager : EdgeMultiplayCallbacks
{
    #region Some Variable

    [Header("Health Component")] 
    [SerializeField] private float maxHp;
    [HideInInspector] public float[] currentHp;

    [Header("UI Component")] 
    public Image[] hpBar;
    public Image[] hpEffect;
    public TextMeshProUGUI clogText;
    [SerializeField] private float increaseHpBar;
    [SerializeField] private float speedIncreaseHpBar;

    #endregion
    
    #region MonoBehaviour Callbacks
    
    private void Awake()
    {
        currentHp = new float[2];
        for (int i = 0; i < currentHp.Length; i++)
        {
            currentHp[i] = maxHp;
        }
    }

    private void Start ()
    {
        ConnectToEdge();
        Clog("Connecting in Edge");
    }

    private void Update()
    {
        HealthInterface();
    }

    #endregion
    
    #region EdgeMultiplay Callbacks

    // Called once connected to your server deployed on Edge
    public override void OnConnectionToEdge()
    {
        print("Connected to server deployed on Edge");
        Clog("Connected to server deployed on Edge");
    }
    
    public override void OnFaliureToConnect(string reason)
    {
        print("Failed to connect to Edge!" + "\n" + reason);
    }
    
    // Called once the server registers the player right after the connection is established
    public override void OnRegisterEvent()
    {
        print("Game Session received from server");
        EdgeManager.JoinOrCreateRoom("Bismillah", 0, 2, minPlayersToStartGame:2);
    }
    
    // Called once the JoinRoom request succeeded 
    public override void OnRoomJoin(Room room)
    {
        print("Joined room");
        print("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print("Count of Players in the room :"+ room.roomMembers.Count); 
    }
    
    // Called once the CreateRoom request succeeded 
    public override void OnRoomCreated(Room room)
    {
        print("Created a room");
        print("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print("Count of Players in the room :"+ room.roomMembers.Count); 
    }
    
    // Called once the Game start on the server
    // The game starts on the server once the count of room members reachs the maximum players per room
    public override void OnGameStart()
    {
        Clog("Game started", true);
    }
    
    public override void OnPlayerLeft(RoomMemberLeft playerLeft)
    {
        Clog(EdgeManager.GetPlayer(playerLeft.idOfPlayerLeft).playerName + " left");
    }

    #endregion

    #region GameManager Functions
    
    public void RestartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    
    public void DecreaseHp(float damage, int playerIndex)
    {
        currentHp[playerIndex] -= damage;
        
        if (currentHp[playerIndex] <= 0)
        {
            currentHp[playerIndex] = 0;
            RestartGame();
        }
    }
    
    private void HealthInterface()
    {
        for (int i = 0; i < currentHp.Length; i++)
        {
            hpBar[i].fillAmount = currentHp[i] / maxHp;
        
            if (hpEffect[i].fillAmount > hpBar[i].fillAmount)
            {
                hpEffect[i].fillAmount -= increaseHpBar * speedIncreaseHpBar;
            }
            else
            {
                hpEffect[i].fillAmount = hpBar[i].fillAmount;
            }
        }
    }

    public void Clog(string message, bool emptyLog = false)
    {
        if (!emptyLog)
        {
            clogText.text += "\n" + message;
        }
        else
        {
            clogText.text = message;
        }
        Debug.Log(message);
    }
        
    #endregion
 
}
