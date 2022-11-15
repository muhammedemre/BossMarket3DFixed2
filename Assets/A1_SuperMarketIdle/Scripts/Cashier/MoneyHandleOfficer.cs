using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class MoneyHandleOfficer : MonoBehaviour
{
    [SerializeField] CashierActor cashierActor;
    [SerializeField] private int moneyOnTheDesk = 0;
    [SerializeField] int oneStackHowMuchMoney;
    public Transform moneyCollectPoint;
    public float paymentTotalDuration;
    public AudioSource audioSource;

    public int MoneyOnTheDesk
    {
        get
        {
            return moneyOnTheDesk;
        }
        set
        {
            moneyOnTheDesk = value;
        }
    }

    public void AddMoneyOnTheDesk(int moneyAmount, Vector3 throwPosition)
    {
        
        MoneyOnTheDesk += moneyAmount;
        int stackAmount = CalculateStack(moneyAmount);
        float paymentFrequency = paymentTotalDuration / moneyAmount;
        StartCoroutine(cashierActor.moneyStackOfficer.ThrowMoney(moneyAmount, throwPosition, paymentFrequency));
    }

    int CalculateStack(int moneyToCalculate)
    {
        return moneyToCalculate / oneStackHowMuchMoney;
    }

    public void GetTheMoneyOnTheDesk()
    {
        //PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(moneyOnTheDesk);

        MoneyOnTheDesk = 0;
        StartCoroutine(cashierActor.moneyStackOfficer.CollectTheMoney());       
    }
}
