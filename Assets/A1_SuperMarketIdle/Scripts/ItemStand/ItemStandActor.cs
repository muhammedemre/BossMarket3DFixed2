using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStandActor : MonoBehaviour
{
    public ItemStandItemHandleOfficer itemStandItemHandleOfficer;
    public ItemStandSolidMaker itemStandSolidMaker;
    public Transform interactionPoint;
    public bool busy; // currently selling to a customer.
    public RoomActor belongingRoom;
    public int itemStandIndexForTheRoom;

    private void Start()
    {
        RegisterToItemStandsList();
        LetRoomDataIamActivated();
        IfCreatedFromDataLoad();
    }
    void RegisterToItemStandsList()
    {
        belongingRoom.roomFixturesOfficer.roomItemStands.Add(transform);
        StartCoroutine(WakeUpNeedCheck());
    }
    IEnumerator WakeUpNeedCheck()
    {
        yield return new WaitForSeconds(3f);
        if (itemStandItemHandleOfficer.emptySlotAmount > 0)
        {
            belongingRoom.roomStuffOrganizeOfficer.ItemStandNeedsRefill(itemStandItemHandleOfficer);
        }
    }
    void LetRoomDataIamActivated()
    {
        int productOwned = itemStandItemHandleOfficer.storageList.Count;
        if (!belongingRoom.roomDataOfficer.roomActiveItemStands.ContainsKey(itemStandIndexForTheRoom))
        {
            belongingRoom.roomDataOfficer.roomActiveItemStands.Add(itemStandIndexForTheRoom, productOwned);
        }
        //DataManager.instance.DataSaveAndLoadOfficer.SaveTheData();
    }

    void IfCreatedFromDataLoad()
    {
        if (belongingRoom.roomDataOfficer.roomActiveItemStands.ContainsKey(itemStandIndexForTheRoom))
        {
            itemStandItemHandleOfficer.AddItemsToStandFromScript(belongingRoom.roomDataOfficer.roomActiveItemStands[itemStandIndexForTheRoom]);
            if (Time.time < 5f)
            {
                itemStandSolidMaker.MakeSolid();
            }
            else
            {
                itemStandSolidMaker.MakeUnSolid();
            }

        }
        else
        {
            itemStandSolidMaker.MakeUnSolid();
        }
    }
}
