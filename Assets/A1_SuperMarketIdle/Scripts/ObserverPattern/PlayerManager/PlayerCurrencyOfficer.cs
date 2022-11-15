using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCurrencyOfficer : MonoBehaviour
{
    [SerializeField] int money = -1;
    [SerializeField] int moneyAtStart;
    [SerializeField] AudioClip moneyAudioClip;
    public int investedMoneyAmount = 0;
 
    public int Money
    {
        get
        {
            return money;
        }
        set
        {
            money = value;
            if (money < 0 )
            {
                money = 0;
            }
            UIManager.instance.UpdateMoneyText(money);
        }
    }

    public void MoneyDepositToTheWallet(int moneyToDeposit)
    {
        if (LevelManager.instance.levelPowerUpOfficer.coinBoostActive)
        {
            float boostCoefficient = LevelManager.instance.levelPowerUpOfficer.coinBoostCoefficient;
            moneyToDeposit = (int)(moneyToDeposit*boostCoefficient);
        }
        
        Money += moneyToDeposit;
    }

    public void AddToInvestmentMoney(int moneyToInvest) 
    {
        print("AddToInvestmentMoney");
        investedMoneyAmount += moneyToInvest;
    }

    public void GetMoneyDataFromDataManager(int moneyData)
    {
        if (moneyData == -1)
        {
            Money = moneyAtStart;
        }
        else
        {
            Money = moneyData; // Get the data from datamanager  
        }      
    }
}
