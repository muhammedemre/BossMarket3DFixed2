using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomActor : MonoBehaviour
{
    public RoomFixturesOfficer roomFixturesOfficer;
    public RoomCustomerOfficer roomCustomerOfficer;
    public RoomTruckOfficer roomTruckOfficer;
    public RoomStuffOrganizeOfficer roomStuffOrganizeOfficer;
    public RoomDataOfficer roomDataOfficer;
    public RoomEnvironmentOfficer roomEnvironmentOfficer;
    public ActivisionPointAnchor roomPortionActivator;
    public int itemPriceInBanknotesForThisRoom = 2, banknoteValue;
    public bool isActive = false;
    public Transform powerBoostPlacementPositions;
    public int roomIndex;
    [SerializeField] float TruckFirstComeDelay;

    private void Start()
    {
        LetLevelDataIamActivated();
        TriggerTheRoomEvent();
        StartCoroutine(RoomActivate());
    }

    public IEnumerator RoomActivate()
    {
        yield return new WaitForSeconds(UIManager.instance.splashVideoDuration + TruckFirstComeDelay);
        roomTruckOfficer.CallTheTruck(true);
        //roomFixturesOfficer.ActivateNavmeshSurfaceOnTheRoom();
    }

    void LetLevelDataIamActivated()
    {
        if (!LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer.activeRooms.ContainsKey(roomIndex))
        {
            LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelDataOfficer.LetMeKnowRoomIsActivated(roomIndex, gameObject);
        }

        //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }

    void TriggerTheRoomEvent()
    {
        EventsManager.instance.RoomEventsTrigger(true, roomIndex);
    }
}
