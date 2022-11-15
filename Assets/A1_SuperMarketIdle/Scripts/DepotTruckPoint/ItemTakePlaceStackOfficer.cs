using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemTakePlaceStackOfficer : MonoBehaviour
{
    [SerializeField] ItemTakePlaceActor itemTakePlaceActor;
    public Transform itemPlaces;
    [SerializeField] float travelEulerRandomAngle, travelDuration, travelJumpPower, stockUpFrequency;
    [SerializeField] Vector3 travelEndEuler;
    [SerializeField] List<Transform> deliveredItemList = new List<Transform>();
    [SerializeField] float takeItemFromTheStackFrequency;
    float nextTakeItemFromTheStackTime = 0;

    public List<ItemCarryStackOfficer> stuffLine = new List<ItemCarryStackOfficer>();

    private void Update()
    {
        TakeItemCheck();
    }

    public IEnumerator StockUpTheItems(List<Transform> _deliveredItemList)
    {
        deliveredItemList = _deliveredItemList;
        for (int i = 0; i < deliveredItemList.Count; i++)
        {
            if (i < itemPlaces.childCount)
            {
                deliveredItemList[i].SetParent(itemPlaces.GetChild(i));
                Vector3 travelEndPosition = itemPlaces.GetChild(i).position;
                deliveredItemList[i].GetComponent<ItemActor>().itemMoveOfficer.ItemTravelToTheItemTakePoint(travelEndPosition, travelEndEuler, travelEulerRandomAngle, travelDuration, travelJumpPower);
                
            }
            yield return new WaitForSeconds(stockUpFrequency);
        }
    }

    void TakeItemCheck()
    {
        if (nextTakeItemFromTheStackTime < Time.time && stuffLine.Count > 0)
        {
            GiveTheItem();
            nextTakeItemFromTheStackTime = Time.time + takeItemFromTheStackFrequency;
        }
    }

    void GiveTheItem()
    {
        ItemCarryStackOfficer currentStuff = stuffLine[0];

        if (deliveredItemList.Count > 0)
        {
            bool canStuffTake = CheckStuffCapacity(currentStuff);
            if (canStuffTake)
            {
                Transform itemToGive = TakeItemFromTheStack();
                deliveredItemList.Remove(itemToGive);
                currentStuff.AddAnItemToStack(itemToGive);
                CheckIfItemTakePlaceIsEmpty();
                if (deliveredItemList.Count <= 0)
                {
                    NoMoreItemLeftGoAway();
                }
            }
            else 
            {
                if (currentStuff.tag == "Stuff")
                {
                    itemTakePlaceActor.depotTruckPointActor.belongingRoom.roomStuffOrganizeOfficer.RegisterToReadyToServe(currentStuff.GetComponent<RootFinderOfficer>().root.GetComponent<StuffActor>());
                    stuffLine.Remove(currentStuff);
                }
            }
        }
        
    }

    public Transform TakeItemFromTheStack()
    {
        int lastItemIndex = deliveredItemList.Count - 1;
        return deliveredItemList[lastItemIndex];
    }

    public void StuffLineAdd(ItemCarryStackOfficer itemCarryStackOfficer)
    {
        if (!stuffLine.Contains(itemCarryStackOfficer))
        {
            if (itemCarryStackOfficer.tag == "Player")
            {
                stuffLine = BreakTheLine(itemCarryStackOfficer);
            }
            else
            {
                stuffLine.Add(itemCarryStackOfficer);
            }
        }
    }

    public void StuffLineRemove(ItemCarryStackOfficer itemCarryStackOfficer)
    {
        if (stuffLine.Contains(itemCarryStackOfficer))
        {
            stuffLine.Remove(itemCarryStackOfficer);
        }
       
    }

    List<ItemCarryStackOfficer> BreakTheLine(ItemCarryStackOfficer itemCarryStackOfficer)
    {
        List<ItemCarryStackOfficer> newLine = new List<ItemCarryStackOfficer>() {itemCarryStackOfficer};
        foreach (ItemCarryStackOfficer stuff in stuffLine)
        {
            newLine.Add(stuff);
        }
        return newLine;
    }

    bool CheckStuffCapacity(ItemCarryStackOfficer stuff)
    {
        return ((stuff.carryCapacity > stuff.carryingItemsList.Count) ? true : false);
    }

    void CheckIfItemTakePlaceIsEmpty()
    {
        if (deliveredItemList.Count == 0)
        {
            itemTakePlaceActor.depotTruckPointActor.truckHandleOfficer.truck.truckMoveOfficer.SendTheTruck();
        }
    }

    void NoMoreItemLeftGoAway()
    {
        int count = stuffLine.Count;
        for (int i = 0; i < count; i++)
        {
            if (stuffLine.Count > 0 && i < stuffLine.Count)
            {
                ItemCarryStackOfficer stuff = stuffLine[i];
                if (stuff.tag == "Stuff")
                {
                    if (stuffLine.Contains(stuff))
                    {
                        itemTakePlaceActor.depotTruckPointActor.belongingRoom.roomStuffOrganizeOfficer.RegisterToReadyToServe(stuff.GetComponent<RootFinderOfficer>().root.GetComponent<StuffActor>());
                        stuffLine.Remove(stuff);
                    }
                }
            }
            
        }
    }

    public int GetItemTakePlaceItemAmount()
    {
        return deliveredItemList.Count;
    }
}
