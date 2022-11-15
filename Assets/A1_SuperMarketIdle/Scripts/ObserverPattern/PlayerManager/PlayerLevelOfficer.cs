using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class PlayerLevelOfficer : SerializedMonoBehaviour
{
    public Dictionary<string, int> playerVariableLevels = new Dictionary<string, int>() { { "Capacity", 0 }, { "Speed", 0 } };

    public void ApplyPlayerUpgrade(string playerVariable, int newLevel)
    {
        if (playerVariableLevels.ContainsKey(playerVariable))
        {
            playerVariableLevels[playerVariable] = newLevel;
        }
        ApplyNewValues();
    }

    void ApplyNewValues()
    {
        
        int speedIndex = (playerVariableLevels["Speed"] < 0)? 0 : playerVariableLevels["Speed"];
        float speedRate = DataManager.instance.gameVariablesData.PlayerUpgradeSpeedValues[speedIndex];
        PlayerManager.instance.playerActor.playerMoveOfficer.speed = PlayerManager.instance.playerActor.playerMoveOfficer.speedAtTheBeginning * speedRate;

        int capacityIndex = (playerVariableLevels["Capacity"] < 0) ? 0 : playerVariableLevels["Capacity"];
        int newCarryCapacity = DataManager.instance.gameVariablesData.PlayerUpgradeCapacityValues[capacityIndex];
        PlayerManager.instance.playerActor.itemCarryStackOfficer.carryCapacity = newCarryCapacity;

        //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }
}
