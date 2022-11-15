using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradeWindowUpgradeOfficer : MonoBehaviour
{
    public RoomActor relatedRoomActor;

    public bool PlayerUpgradeButton(int playerButtonIndex)
    {
        return relatedRoomActor.roomFixturesOfficer.roomUpgradePoint.GetComponent<UpgradePointActor>().PlayerUpgrade(playerButtonIndex);
    }

    public bool WorkerUpgradeButton(int workerIndex)
    {
        return relatedRoomActor.roomFixturesOfficer.roomUpgradePoint.GetComponent<UpgradePointActor>().UpgradeTheWorker(workerIndex);
    }

    public bool TruckUpgradeButton(int truckButtonIndex)
    {
        return relatedRoomActor.roomFixturesOfficer.roomUpgradePoint.GetComponent<UpgradePointActor>().TruckUpgrade(truckButtonIndex);
    }

    public bool EnvironmentUpgradeButton(int environmentIndex)
    {
        return relatedRoomActor.roomFixturesOfficer.roomUpgradePoint.GetComponent<UpgradePointActor>().EnvironmentUpgrade(environmentIndex);
    }
}
