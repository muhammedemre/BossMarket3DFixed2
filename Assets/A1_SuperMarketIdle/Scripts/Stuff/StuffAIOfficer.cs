using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StuffAIOfficer : MonoBehaviour
{
    [SerializeField] StuffActor stuffActor;
    public RoomActor roomInIt;
    public ItemTakePlaceActor itemTakePlaceActor;
    public ItemStandActor targetItemStand;
    [SerializeField] float stuckCheckFrequency;
    float nextStuckCheckCheck = 0;

    enum StuffState
    {
        TakeItem, LeaveItem
    }

    [SerializeField] StuffState currentState;

    private void Start()
    {
        FindTheRoomInIt();
        TakeAnItem();
    }

    private void Update()
    {
        StuckCheck();
    }


    void GoTarget(Vector3 targetPos, Transform lookAtTransfrom, bool ableFacing)
    {
        stuffActor.stuffMoveOfficer.MoveToTarget(targetPos, lookAtTransfrom, ableFacing);

    }

    public void TakeAnItem()// Go Get Item from ItemTakePoint
    {
        currentState = StuffState.TakeItem;
        int randomItemTakePlaceInt = Random.Range(0, itemTakePlaceActor.itemTakePlaces.childCount);
        Vector3 targetPos = itemTakePlaceActor.itemTakePlaces.GetChild(randomItemTakePlaceInt).transform.position;
        GoTarget(targetPos, itemTakePlaceActor.transform, false); // When reaches it calles reachedTheTarget on StuffMoveOfficer
    }

    public void ReachedTheTarget()
    {
        if (currentState == StuffState.TakeItem)
        {
            ItemTakeProcess();
        }
        else if (currentState == StuffState.LeaveItem)
        {
            FillTheItemStand();
        }
    }

    public void GoToLeaveTheItem(ItemStandItemHandleOfficer itemStand)
    {
        currentState = StuffState.LeaveItem;
        targetItemStand = itemStand.itemStandActor;
        Vector3 targetPos = itemStand.itemStandActor.interactionPoint.transform.position;
        GoTarget(targetPos, itemStand.transform, false);
    }

    void ItemTakeProcess()
    {
        itemTakePlaceActor.itemTakePlaceStackOfficer.StuffLineAdd(stuffActor.itemCarryStackOfficer);
    }

    void FindTheRoomInIt()
    {
        UpgradePointActor upgradePointActor = transform.parent.GetComponent<RootFinderOfficer>().root.GetComponent<UpgradePointActor>();
        itemTakePlaceActor = upgradePointActor.belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().itemTakePlaceActor;
    }
    public void FulledByItems()
    {
        currentState = StuffState.LeaveItem;
        roomInIt.roomStuffOrganizeOfficer.RegisterToReadyToServe(stuffActor);
    }
    public void WhereToLeaveTheItems(ItemStandItemHandleOfficer itemStand)
    {
        GoToLeaveTheItem(itemStand);
    }
    void FillTheItemStand()
    {
        if (targetItemStand == null)
        {
            return;
        }
        stuffActor.itemCarryStackOfficer.CheckToGiveItemToTheStand(targetItemStand.GetComponent<ItemStandActor>(), true);
        targetItemStand = null;
    }
    void StuckCheck()
    {
        if (nextStuckCheckCheck < Time.time)
        {
            nextStuckCheckCheck = Time.time + stuckCheckFrequency;
            if (targetItemStand == null)
            {
                if (stuffActor.itemCarryStackOfficer.carryingItemsList.Count > 0)
                {
                    roomInIt.roomStuffOrganizeOfficer.RegisterToReadyToServe(stuffActor);
                }
                else if (true)
                {
                    TakeAnItem();
                }
            }           
        }
    }

}
