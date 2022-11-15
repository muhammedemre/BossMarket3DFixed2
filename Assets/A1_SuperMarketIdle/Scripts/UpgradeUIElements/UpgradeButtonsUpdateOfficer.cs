using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class UpgradeButtonsUpdateOfficer : MonoBehaviour
{
    [SerializeField] UpgradeWindowActor upgradeWindowActor;

    public void UpdateTheButtons()
    {
        PlayerButtonsUpdate();
        WorkerButtonsUpdate();
        EnvironmentButtonUpdate();
        TruckButtonUpdate();
    }

    void PlayerButtonsUpdate()
    {
        upgradeWindowActor.upgradeButtons["playerspeed"].UpdateTheButton(PlayerManager.instance.SpeedLevel);
        upgradeWindowActor.upgradeButtons["playercapacity"].UpdateTheButton(PlayerManager.instance.CapacityLevel);
    }

    void WorkerButtonsUpdate()
    {
        Dictionary<int, string> workerDict = new Dictionary<int, string>() { {0, "worker1" }, { 1, "worker2" }, { 2, "worker3" }, { 3, "worker4" } };

        for (int i = 0; i < workerDict.Count; i++)
        {
            int workerLevel = upgradeWindowActor.upgradeWindowUpgradeOfficer.relatedRoomActor.roomDataOfficer.workerLevels[i];
            string buttonKey = workerDict[i];
            upgradeWindowActor.upgradeButtons[buttonKey].UpdateTheButton(workerLevel+1);
        }
    }

    void EnvironmentButtonUpdate()
    {
        int treeValue = 0;
        int wallValue = 0;
        upgradeWindowActor.upgradeButtons["tree"].UpdateTheButton(treeValue);
        upgradeWindowActor.upgradeButtons["wall"].UpdateTheButton(wallValue);
    }

    void TruckButtonUpdate()
    {
        int truckCapacityLevel = upgradeWindowActor.upgradeWindowUpgradeOfficer.relatedRoomActor.roomDataOfficer.truckCapacityLevel;
        int truckSpeedLevel = upgradeWindowActor.upgradeWindowUpgradeOfficer.relatedRoomActor.roomDataOfficer.truckSpeedLevel;
        upgradeWindowActor.upgradeButtons["truckcapacity"].UpdateTheButton(truckCapacityLevel);
        upgradeWindowActor.upgradeButtons["truckspeed"].UpdateTheButton(truckSpeedLevel);
    }
}
