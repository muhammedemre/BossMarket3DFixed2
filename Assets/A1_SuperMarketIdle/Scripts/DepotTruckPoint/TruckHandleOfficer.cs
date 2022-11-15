using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TruckHandleOfficer : MonoBehaviour
{
    [SerializeField] DepotTruckPointActor depotTruckPointActor;
    public TruckActor truck;
    Dictionary<string, int> truckVariableLevels = new Dictionary<string, int>() { { "Capacity", 0 }, { "Speed", 0 } };

    [SerializeField] float truckIdleCheckFrequency;
    float nextTruckIdleCheck = 0f;
    Vector3 truckStuckPos = new Vector3(555f, 555f, 555f);

    private void Update()
    {
        TruckIdleCheck();
    }

    public void CallTheTruck()
    {
        truck.truckMoveOfficer.CallTheTruck();
    }
    public void SendTheTruck()
    {
        truck.truckMoveOfficer.SendTheTruck();
    }

    public void OpenTruckUpgradeWindow()
    {
        UIManager.instance.UITaskOfficers.OpenAndCloseTruckUpgradeWindow(true, depotTruckPointActor.belongingRoom);
    }

    public void ApplyTruckUpgrade(string truckVariable, int newLevel)
    {
        if (truckVariableLevels.ContainsKey(truckVariable))
        {
            truckVariableLevels[truckVariable] = newLevel;
        }
        //print(truckVariable + " new level of it : "  + newLevel); enough
        ApplyNewValues();
    }

    void ApplyNewValues()
    {
        int speedIndex = (truckVariableLevels["Speed"] < 0) ? 0 : truckVariableLevels["Speed"];
        float speedRate = DataManager.instance.gameVariablesData.TruckUpgradeSpeedValues[speedIndex];
        truck.GetComponent<TruckActor>().truckMoveOfficer.TruckSpeedSetter(speedRate);

        int capacityIndex = (truckVariableLevels["Capacity"] < 0) ? 0 : truckVariableLevels["Capacity"];
        int newCarryCapacity = DataManager.instance.gameVariablesData.TruckUpgradeCapacityValues[capacityIndex];
        truck.GetComponent<TruckActor>().truckItemOrganizeOfficer.truckCapacity = newCarryCapacity;
        //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }

    void TruckIdleCheck()
    {
        if (nextTruckIdleCheck < Time.time)
        {
            nextTruckIdleCheck = Time.time + truckIdleCheckFrequency;
            if (depotTruckPointActor.itemTakePlaceActor.itemTakePlaceStackOfficer.GetItemTakePlaceItemAmount() <= 0)
            {
                if (truckStuckPos == new Vector3(555f, 555f, 555f))
                {
                    truckStuckPos = truck.transform.position;
                }
                else if (truckStuckPos == truck.transform.position)
                {
                    truckStuckPos = new Vector3(555f, 555f, 555f);
                    SendTheTruck();
                }
            }
        }
    }

    #region Button

    [Title("Truck Call and Send Buttons")]
    [Button("CallTheTruck", ButtonSizes.Large)]
    void ButtonCallTheTruck()
    {
        CallTheTruck();
    }
    [Button("SendTheTruck", ButtonSizes.Large)]
    void ButtonSendTheTruck()
    {
        SendTheTruck();
    }
    #endregion
}
