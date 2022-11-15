using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class RoomDataOfficer : SerializedMonoBehaviour
{
    [SerializeField] RoomActor roomActor;
    //public List<int> workerLevels = new List<int>();
    public int workerMaxLevel, truckMaxLevel;
    public Dictionary<int, int> workerLevels = new Dictionary<int, int>() { {0,-1}, { 1, -1 }, { 2, -1 }, { 3, -1 } }; // -1 means its not activated
    public int truckCapacityLevel = 0, truckSpeedLevel = 0, treeLevel = 0, wallLevel = 0;
    public bool isPortionOpen = false;
    //public Dictionary<int, GameObject> roomActiveItemStands = new Dictionary<int, GameObject>();
    //public List<int> roomActiveItemStands = new List<int>();
    public Dictionary<int, int> roomActiveItemStands = new Dictionary<int, int>(); // itemStandIndex, productNumberOnIt



    public void AssignTheVariables()
    {
        PortionDataAssign();
        ItemStandsDataAssign();
        roomActor.roomFixturesOfficer.roomCashier.GetComponent<CashierActor>().moneyStackOfficer.PrepareTheDeskMoney();
        roomActor.roomTruckOfficer.depotTruckPointActor.truckHandleOfficer.ApplyTruckUpgrade("Capacity", truckCapacityLevel);
        roomActor.roomTruckOfficer.depotTruckPointActor.truckHandleOfficer.ApplyTruckUpgrade("Speed", truckSpeedLevel);
        WorkerDataAssigns();
        
    }

    void WorkerDataAssigns()
    {
        foreach (int workerId in workerLevels.Keys)
        {
            if (workerLevels[workerId] > -1)
            {
                if (!IsWorkerActiveCheck(workerId))
                {
                    roomActor.roomFixturesOfficer.roomUpgradePoint.GetComponent<UpgradePointActor>().StuffSpawn(workerId);
                }
                if (IsWorkerActiveCheck(workerId))
                {
                    int stuffLevel = workerLevels[workerId];
                    roomActor.roomStuffOrganizeOfficer.roomStuffDict[workerId].GetComponent<StuffActor>().stuffLevelOfficer.UpgradeTheStuff(stuffLevel);
                }
                
            }
        }
    }

    bool IsWorkerActiveCheck(int workerId)
    {
        return roomActor.roomStuffOrganizeOfficer.roomStuffDict.ContainsKey(workerId);
    }

    void PortionDataAssign()
    {
        if (isPortionOpen)
        {
            roomActor.roomPortionActivator.ActivisionCalculateOfficer.ActivateTheObject();
        }
    }

    void ItemStandsDataAssign()
    {
        foreach (int itemStandIndex in roomActiveItemStands.Keys)
        {
            if (roomActor.roomFixturesOfficer.roomItemStandActivator.ContainsKey(itemStandIndex))
            {
                roomActor.roomFixturesOfficer.roomItemStandActivator[itemStandIndex].GetComponent<ActivisionPointAnchor>().ActivisionCalculateOfficer.ActivateTheObject();
            }
        }
    }

    public void DataLoad()
    {
        if (DataManager.instance.DataSaveAndLoadOfficer.dataLoaded)
        {

        }
    }
    public void DataSave()
    {

    }
}
