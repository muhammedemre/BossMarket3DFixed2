using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomStuffOrganizeOfficer : MonoBehaviour
{
    [SerializeField] RoomActor roomActor;
    public Dictionary<int, StuffActor> roomStuffDict = new Dictionary<int, StuffActor>();
    public List<StuffActor> activeStuffsList = new List<StuffActor>();
    public List<StuffActor> readyToServeStuff = new List<StuffActor>();
    [SerializeField] List<ItemStandItemHandleOfficer> needToRefillItemStandsList = new List<ItemStandItemHandleOfficer>();
    

    


    public void RegisterActiveStuff(StuffActor stuff)
    {
        activeStuffsList.Add(stuff);
    }

    public void ItemStandNeedsRefill(ItemStandItemHandleOfficer itemStand)
    {
        if (!needToRefillItemStandsList.Contains(itemStand))
        {
            needToRefillItemStandsList.Add(itemStand);
            AssignAStuffToAnItemStandInNeed();
        }    
    }

    public void RegisterToReadyToServe(StuffActor stuff)
    {

        if (!readyToServeStuff.Contains(stuff))
        {
            readyToServeStuff.Add(stuff);
            AssignAStuffToAnItemStandInNeed();
        }
        
    }

    void AssignAStuffToAnItemStandInNeed()
    {
        if (needToRefillItemStandsList.Count > 0) // if there is an itemstand in need
        {
            if (readyToServeStuff.Count > 0) // if there is a stuff ready to serve
            {
                readyToServeStuff[0].stuffAIOfficer.WhereToLeaveTheItems(needToRefillItemStandsList[0]); // Send a stuff to the itemStand               
                if (needToRefillItemStandsList[0].emptySlotAmount > readyToServeStuff[0].itemCarryStackOfficer.carryCapacity) // called stuff is not enough to refill the stand.
                {
                    ItemStandItemHandleOfficer tempItemStand = needToRefillItemStandsList[0];
                    needToRefillItemStandsList.RemoveAt(0);
                    needToRefillItemStandsList.Add(tempItemStand); // add the itemStand to the list again.
                }
                readyToServeStuff.RemoveAt(0);
            }
        }
    }

    public void HireAStuff(int stuffIndex, StuffActor stuff)
    {
        if (roomStuffDict.ContainsKey(stuffIndex))
        {
            return;
        }
        stuff.stuffAIOfficer.roomInIt = roomActor;
        roomStuffDict[stuffIndex] = stuff;
        RegisterActiveStuff(stuff);
    }

    public void RemoveItemStandFromInNeedList(ItemStandItemHandleOfficer itemStand)
    {
        if (needToRefillItemStandsList.Contains(itemStand))
        {
            needToRefillItemStandsList.Remove(itemStand);
        }
    }

    
}
