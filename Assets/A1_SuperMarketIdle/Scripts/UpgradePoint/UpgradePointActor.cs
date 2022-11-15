using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class UpgradePointActor : MonoBehaviour
{
    [SerializeField] RoomDataOfficer roomDataOfficer;
    [SerializeField] Transform stuffPrefab, stuffSpawnPoint;
    public RoomActor belongingRoom;
    public UpgradePointInteractionOfficer UpgradePointInteractionOfficer;


    public void OpenUpgradeWindow()
    {
        UIManager.instance.UITaskOfficers.UpgradeWindowSetState(true, belongingRoom);
    }

    public void StuffSpawn(int stuffIndex)
    {
        Transform stuff = Instantiate(stuffPrefab, stuffSpawnPoint.position, Quaternion.identity, stuffSpawnPoint);
        belongingRoom.roomStuffOrganizeOfficer.HireAStuff(stuffIndex, stuff.GetComponent<StuffActor>());
    }

    public bool PlayerUpgrade(int buttonIndex)
    {
        int playerMaxLevel = DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count;
        if (buttonIndex == 1)
        {
            PlayerLevelOfficer _playerLevelOfficer = PlayerManager.instance.playerActor.playerLevelOfficer;
            int playerCapacityLevel = (_playerLevelOfficer.playerVariableLevels["Capacity"] < playerMaxLevel) ? (_playerLevelOfficer.playerVariableLevels["Capacity"] + 1) : playerMaxLevel;
            PlayerManager.instance.CapacityLevel = playerCapacityLevel;
            //_playerLevelOfficer.ApplyPlayerUpgrade("Capacity", playerCapacityLevel);
            return true;
        }
        else if (buttonIndex == 0)
        {
            PlayerLevelOfficer _playerLevelOfficer = PlayerManager.instance.playerActor.playerLevelOfficer;
            int playerSpeedLevel = (_playerLevelOfficer.playerVariableLevels["Speed"] < playerMaxLevel) ? (_playerLevelOfficer.playerVariableLevels["Speed"] + 1) : playerMaxLevel;
            PlayerManager.instance.SpeedLevel = playerSpeedLevel;
            //_playerLevelOfficer.ApplyPlayerUpgrade("Speed", playerSpeedLevel);
            return true;
        }
        return false;
    }

    public bool UpgradeTheWorker(int workerId)
    {
        int workerMaxLevel = DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts.Count + 1;
        if (roomDataOfficer.workerLevels[workerId] < workerMaxLevel)
        {
            if (roomDataOfficer.workerLevels[workerId] == -1)
            {
                StuffSpawn(workerId);
            }
            roomDataOfficer.workerLevels[workerId]++;
            belongingRoom.roomStuffOrganizeOfficer.roomStuffDict[workerId].GetComponent<StuffActor>().stuffLevelOfficer.UpgradeTheStuff(roomDataOfficer.workerLevels[workerId]);
            //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();

            return true;
        }
        return false;
    }

    public bool TruckUpgrade(int buttonIndex)
    {
        int truckMaxLevel = DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts.Count;
        if (buttonIndex == 0)
        {
            roomDataOfficer.truckCapacityLevel = (roomDataOfficer.truckCapacityLevel < truckMaxLevel) ? (roomDataOfficer.truckCapacityLevel + 1) : truckMaxLevel;
            belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().truckHandleOfficer.ApplyTruckUpgrade("Capacity", roomDataOfficer.truckCapacityLevel);
            return true;
        }
        else if (buttonIndex == 1)
        {
            roomDataOfficer.truckSpeedLevel = (roomDataOfficer.truckSpeedLevel < truckMaxLevel) ? (roomDataOfficer.truckSpeedLevel + 1) : truckMaxLevel;
            belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().truckHandleOfficer.ApplyTruckUpgrade("Speed", roomDataOfficer.truckSpeedLevel);
            return true;
        }
        return false;
    }
    public bool EnvironmentUpgrade(int buttonIndex)
    {
        int environmentMaxLevel = DataManager.instance.gameVariablesData.EnvironmentButtonCosts.Count;
        if (buttonIndex == 0)
        {
            roomDataOfficer.truckCapacityLevel = (roomDataOfficer.truckCapacityLevel < environmentMaxLevel) ? (roomDataOfficer.truckCapacityLevel + 1) : environmentMaxLevel;
            belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().truckHandleOfficer.ApplyTruckUpgrade("Capacity", roomDataOfficer.truckCapacityLevel);
            return true;
        }
        else if (buttonIndex == 1)
        {
            roomDataOfficer.truckSpeedLevel = (roomDataOfficer.truckSpeedLevel < environmentMaxLevel) ? (roomDataOfficer.truckSpeedLevel + 1) : environmentMaxLevel;
            belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().truckHandleOfficer.ApplyTruckUpgrade("Speed", roomDataOfficer.truckSpeedLevel);
            return true;
        }
        return false;
    }

    //#region Button

    //[Title("StuffSpawn Button")]
    //[Button("StuffSpawn", ButtonSizes.Large)]
    //void ButtonStuffSpawn()
    //{
    //    StuffSpawn();
    //}
    //#endregion
}
