using System.Collections;
using UnityEngine;

public class PlayerManager : Manager
{
    public static PlayerManager instance;
    public PlayerActor playerActor;
    public PlayerCurrencyOfficer playerCurrencyOfficer;

    [SerializeField] int speedLevel, capacityLevel;

    public int SpeedLevel
    {
        get
        {
            return speedLevel;
        }
        set
        {
            speedLevel = value;
            StartCoroutine(AssignDelay("Speed", speedLevel));
        }
    }

    public int CapacityLevel
    {
        get
        {
            return capacityLevel;
        }
        set
        {
            capacityLevel = value;
            StartCoroutine(AssignDelay("Capacity", capacityLevel));
        }
    }

    private void Awake()
    {
        SingletonCheck();
    }
    
    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public override void PostGameStartProcess()
    {
        
    }
    public override void MenuProcess()
    {
        //playerCurrencyOfficer.GetMoneyDataFromDataManager();
    }

    IEnumerator AssignDelay(string variableType, int newLevel)
    {
        yield return new WaitForSeconds(0f);
        playerActor.playerLevelOfficer.ApplyPlayerUpgrade(variableType, newLevel);
    }
}
