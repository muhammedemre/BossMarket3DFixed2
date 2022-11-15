using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestActor : MonoBehaviour
{
    private void Start()
    {
        if (!GameManager.instance.isTest)
        {
            Destroy(gameObject);
        }
    }
    public void GiveMoney() 
    {
        PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(500);
    }
}
