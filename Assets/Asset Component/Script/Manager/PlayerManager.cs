using System;
using UnityEngine;
using EdgeMultiplay;
using UnityEngine.UI;
using TMPro;
 
public class PlayerManager : NetworkedPlayer
{
    [Header("Health Component")] 
    [SerializeField] private float maxHp;
    private float currentHp;
    
    [Header("UI Component")] 
    public Image hpBar;
    public Image hpEffect;
    [SerializeField] private float increaseHpBar;


    private void Start()
    {
        currentHp = maxHp;
    }

    private void Update()
    {
        healthInterface();
    }

    #region Health Method

    private void healthInterface()
    {
        hpBar.fillAmount = currentHp / maxHp;
        
        if (hpEffect.fillAmount > hpBar.fillAmount)
        {
            hpEffect.fillAmount -= Time.deltaTime * increaseHpBar;
        }
        else
        {
            hpEffect.fillAmount = hpBar.fillAmount;
        }
    }

    #endregion

    #region Network Initialization
    
    // Use this for initialization
    private void OnEnable ()
    {
        ListenToMessages();
    }
    
    // Once the GameObject is destroyed
    private void OnDestroy ()
    {
        StopListening();
    }

    // Called once a GamePlay Event is received from the server
    public override void OnMessageReceived(GamePlayEvent gamePlayEvent)
    {
        print ("GamePlayEvent received from server, event name: " + gamePlayEvent.eventName );
    }
    
    #endregion
    
    
 
}
