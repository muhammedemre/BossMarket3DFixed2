using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPreperationOfficer : MonoBehaviour
{
    [SerializeField] LevelActor levelActor;

    public void PrepareTheLevel()
    {
        //levelActor.levelRoomOfficer.ActivateARooom();
        levelActor.levelNavmeshOfficer.BuildTheNavmeshes();
    }
}
