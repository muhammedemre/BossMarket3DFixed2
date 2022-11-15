using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomTruckOfficer : MonoBehaviour
{
    public DepotTruckPointActor depotTruckPointActor;
    public bool truckIsReadyToCall = false;


    private void Start()
    {
        //CallTheTruck();// only to test on APK
    }

    public void CallTheTruck(bool fromRoomActivision) // Since there is no logic to arrange truck sends its called in TruckMoveOfficer with LeftTheMap().
    {
        if (!fromRoomActivision)
        {
            depotTruckPointActor.truckHandleOfficer.truck.truckItemOrganizeOfficer.FillTheTruck();
        }
        depotTruckPointActor.truckHandleOfficer.truck.truckMoveOfficer.CallTheTruck();
    }
}
