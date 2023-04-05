using System;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerManagg : MonoBehaviour
{
    [Header("Health Component")] 
    [SerializeField] private float maxHp;
    private float currentHp;
    private bool isDeath;
    
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
    
    public float DecreaseHp(float damage)
    {
        currentHp -= damage;
        if (currentHp <= 0)
        {
            currentHp = 0;
            isDeath = true;
        }

        return currentHp;
    }
    
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
}