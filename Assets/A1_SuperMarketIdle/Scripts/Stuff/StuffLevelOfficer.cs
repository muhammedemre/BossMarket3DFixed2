using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffLevelOfficer : MonoBehaviour
{
    [SerializeField] StuffActor stuffActor;
    public void UpgradeTheStuff(int upgradeTo)
    {       
        float speedRate = DataManager.instance.gameVariablesData.WorkerUpgradeSpeedValues[upgradeTo];
        stuffActor.stuffMoveOfficer.SetTheSpeed(speedRate);
        //stuffActor.stuffMoveOfficer.stuff.speed = stuffActor.stuffMoveOfficer.speedAtTheBeginning * speedRate;
        //print("Staff is upgraded to this level: " + upgradeTo + " speedRate : " + speedRate + " speed: " + stuffActor.stuffMoveOfficer.stuff.speed);
    }
}
