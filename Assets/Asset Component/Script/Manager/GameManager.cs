using UnityEngine;
using EdgeMultiplay;
using UnityEngine.SceneManagement;
 
[RequireComponent(typeof(EdgeManager))]
public class GameManager : EdgeMultiplayCallbacks {
 
 
    // Use this for initialization
    void Start () {
        ConnectToEdge();
    }
 
    // Called once connected to your server deployed on Edge
    public override void OnConnectionToEdge(){
        print ("Connected to server deployed on Edge");
    }
 
    // Called once the server registers the player right after the connection is established
    public override void OnRegisterEvent(){
        print ("Game Session received from server");
        EdgeManager.JoinOrCreateRoom("playerName", 0, 2);
    }
 
    // Called once the JoinRoom request succeeded 
    public override void OnRoomJoin(Room room){
        print ("Joined room");
        print ("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print ("Count of Players in the room :"+ room.roomMembers.Count); 
    }
 
    // Called once the CreateRoom request succeeded 
    public override void OnRoomCreated(Room room){
        print ("Created a room");
        print ("Maximum Players in the room :"+ room.maxPlayersPerRoom); 
        print ("Count of Players in the room :"+ room.roomMembers.Count); 
    }
 
    // Called once the Game start on the server
    // The game starts on the server once the count of room members reachs the maximum players per room
    public override void OnGameStart(){
        print ("Game Started"); 
    }
    
    #region GameManager Functions
    

        public void UpdateScore(int playerScore, int playerIndex)
        {
            // score[playerIndex] = playerScore;
            // if (score.Max() < 10)
            // {
            //     theBall.GetComponent<BallControl>().ResetBall();
            //     StartCoroutine(theBall.GetComponent<BallControl>().GoBall());
            // }
            // else
            // {
            //     theBall.GetComponent<BallControl>().ResetBall();
            // }
        }

        // public IEnumerator RestartGame()
        // {
        //     yield return new WaitForSeconds(1);
        //     score = new int[2] { 0, 0 };
        //     theBall.SetActive(true);
        //     theBall.GetComponent<BallControl>().ResetBall();
        //     yield return new WaitForSeconds(1);
        //     StartBallMovement();
        // }

        public void RestartGame()
        {
            
        }
        public void clog(string msg, bool emptyLog = false)
        {
            // if (!emptyLog)
            //     uiConsole.text += "\n" + msg;
            // else
            //     uiConsole.text = msg;
            // Debug.Log(msg);
        }

        #endregion
 
}
