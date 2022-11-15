using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;
using System.IO;
using Sirenix.Serialization;

public class DataSaveAndLoadOfficer : MonoBehaviour
{
    public bool dataLoaded = false;

    public void SaveTheData()
    {
        PrepareTheDataPackage();
    }
    public void LoadTheData()
    {
        string filePath = Application.persistentDataPath + "BossMarket";
        LoadState();
    }

    public class RoomData
    {
        public Dictionary<int, int> workerLevels = new Dictionary<int, int>() { { 0, -1 }, { 1, -1 }, { 2, -1 }, { 3, -1 } }; // -1 means its not activated
        public int truckCapacityLevel = 0, truckSpeedLevel = 0, treeLevel = 0, wallLevel = 0, thrownMoneyCount;
        public bool isPortionOpen = false;
        public Dictionary<int, int> activeItemStands = new Dictionary<int, int>(); // itemStandIndex, productNumberOnIt
    }

    // Easy Save Version

    void PrepareTheDataPackage()
    {
        //print("PrepareTheDataPackage()1");
        if (Time.time > 5f) // need to be sure game is not saved before its loaded
        {
            //print("PrepareTheDataPackage()2");
            ES3.Save("playerCapacityLevel", PlayerManager.instance.CapacityLevel);
            ES3.Save("playerSpeedLevel", PlayerManager.instance.SpeedLevel);
            ES3.Save("playerMoney", PlayerManager.instance.playerCurrencyOfficer.Money);
            ES3.Save("investedMoney", PlayerManager.instance.playerCurrencyOfficer.investedMoneyAmount);
            ES3.Save("tutorialFinished", DataManager.instance.tutorialFinished);
            ES3.Save("musicGame", UIManager.instance.settingsMenuActor.musicState);
            ES3.Save("soundGame", UIManager.instance.settingsMenuActor.soundState);
            ES3.Save("vibrationGame", UIManager.instance.settingsMenuActor.vibrationState);

            //ES3.Save("activeRooms", LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer.activeRooms);
            Dictionary<int, RoomData> activeRoomsData = new Dictionary<int, RoomData>();
            LevelDataOfficer levelDataOfficer = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer;
            Dictionary<int, GameObject> tempActiveRooms = levelDataOfficer.activeRooms;
            foreach (int roomIndex in LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer.activeRooms.Keys)
            {
                activeRoomsData[roomIndex] = new RoomData();
                activeRoomsData[roomIndex].workerLevels = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.workerLevels;
                activeRoomsData[roomIndex].truckCapacityLevel = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.truckCapacityLevel;
                activeRoomsData[roomIndex].truckSpeedLevel = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.truckSpeedLevel;
                activeRoomsData[roomIndex].isPortionOpen = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.isPortionOpen;
                activeRoomsData[roomIndex].activeItemStands = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.roomActiveItemStands;
                activeRoomsData[roomIndex].thrownMoneyCount = tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomFixturesOfficer.roomCashier.GetComponent<CashierActor>().moneyStackOfficer.thrownMoneyCounter;
            }

            ES3.Save("activeRoomsData", activeRoomsData);
            ES3.Save("leftInvestments", levelDataOfficer.investmentLeftAmountsForActivisionPoints());
        }
        //print("PrepareTheDataPackage()3");

    }

    IEnumerator UnpackTheDataPackage()
    {
        //print("UnpackTheDataPackage()1");
        yield return new WaitForSeconds(2.5f);
        //print("UnpackTheDataPackage()2");
        PlayerManager.instance.CapacityLevel = ES3.Load("playerCapacityLevel", 0);
        PlayerManager.instance.SpeedLevel = ES3.Load("playerSpeedLevel", 0);
        //PlayerManager.instance.playerCurrencyOfficer.Money = ES3.Load("playerMoney", 0);
        PlayerManager.instance.playerCurrencyOfficer.GetMoneyDataFromDataManager(ES3.Load("playerMoney", -1)); // -1 is triggering the moneyAtStart addition
        PlayerManager.instance.playerCurrencyOfficer.investedMoneyAmount = ES3.Load("investedMoney", 0);
        DataManager.instance.tutorialFinished = ES3.Load("tutorialFinished", false);
        UIManager.instance.settingsMenuActor.musicState = ES3.Load("musicGame", true);
        UIManager.instance.settingsMenuActor.soundState = ES3.Load("soundGame", true);
        UIManager.instance.settingsMenuActor.vibrationState = ES3.Load("vibrationGame", true);

        Dictionary<int, GameObject> tempActiveRooms = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.levelRooms;
        Dictionary<int, RoomData> loadedRoomsData = ES3.Load("activeRoomsData", new Dictionary<int, RoomData>());

        LevelDataOfficer levelDataOfficer = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer;
        foreach (int roomIndex in loadedRoomsData.Keys)
        {
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.workerLevels = loadedRoomsData[roomIndex].workerLevels;
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.truckCapacityLevel = loadedRoomsData[roomIndex].truckCapacityLevel;
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.truckSpeedLevel = loadedRoomsData[roomIndex].truckSpeedLevel;
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.isPortionOpen = loadedRoomsData[roomIndex].isPortionOpen;
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomDataOfficer.roomActiveItemStands = loadedRoomsData[roomIndex].activeItemStands;
            tempActiveRooms[roomIndex].GetComponent<RoomActor>().roomFixturesOfficer.roomCashier.GetComponent<CashierActor>().moneyStackOfficer.thrownMoneyCounter = loadedRoomsData[roomIndex].thrownMoneyCount;

            if (!levelDataOfficer.activeRooms.ContainsKey(roomIndex))
            {
                levelDataOfficer.activeRooms.Add(roomIndex, tempActiveRooms[roomIndex]);
            }
            else
            {
                levelDataOfficer.activeRooms[roomIndex] = tempActiveRooms[roomIndex];
            }
        }
        levelDataOfficer.AssignLevelDatas();
        levelDataOfficer.AssignLeftInvestmentAmounts(ES3.Load("leftInvestments", new List<int>()));
    }

    public void LoadState()
    {
        StartCoroutine(UnpackTheDataPackage());
    }
    public void RefreshTheData()
    {
        List<string> keyList = new List<string>() { "playerCapacityLevel", "playerSpeedLevel", "playerMoney", "tutorialFinished", "activeRoomsData", "musicGame", "soundGame", "vibrationGame", "leftInvestments", "investedMoney" };
        foreach (string key in keyList)
        {
            ES3.DeleteKey(key);
        }
        
    }
    public void DisplayTheData()
    {
        List<string> keyList = new List<string>() { "playerCapacityLevel", "playerSpeedLevel", "playerMoney", "tutorialFinished", "activeRoomsData", "musicGame", "soundGame", "vibrationGame", "leftInvestments", "investedMoney" };
        foreach (string key in keyList)
        {
            print(key+ " : " + ES3.Load(key)) ;
        }
        foreach (int key in ES3.Load("activeRoomsData", new Dictionary<int, RoomData>()).Keys)
        {
            print("ROOM INDEX : " +key);
        }

    }

    // Easy Save Version //


    #region Button

    [Title("Refresh The Data")]
    [Button("Refresh The Data", ButtonSizes.Large)]
    void ButtonRefreshTheData()
    {
        RefreshTheData();
    }

    [Title("Display The Data")]
    [Button("Display The Data", ButtonSizes.Large)]
    void ButtonDisplayTheData()
    {
        DisplayTheData();
    }
    #endregion
}
