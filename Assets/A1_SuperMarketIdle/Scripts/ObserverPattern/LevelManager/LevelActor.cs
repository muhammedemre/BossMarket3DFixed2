using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelActor : MonoBehaviour
{
    public LevelPreperationOfficer LevelPreperationOfficer;
    public WareHouseOfficer levelsWareHouseOfficer;
    public LevelNavmeshOfficer levelNavmeshOfficer;
    public LevelRoomOfficer levelRoomOfficer;
    public Transform itemTempContainer;
    public LevelDataOfficer levelDataOfficer;

    public void PreLevelInstantiateProcess()
    {
        LevelPreperationOfficer.PrepareTheLevel();
    }
}
