using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ItemStandItemHandleOfficer : MonoBehaviour
{
    public ItemStandActor itemStandActor;
    public List<Transform> storageList = new List<Transform>();

    public int capacity, emptySlotAmount;
    [SerializeField] Transform itemPositions, particles, boardIcons;
    public ItemType standsItemType;
    [SerializeField] TextMeshProUGUI itemCounterText;

    private void Start()
    {
        SetTheIconAccordingToTheItemType();
        UpdateItemCounterText(storageList.Count);
    }

    public void AddAnItemToStand(Transform item)
    {
        storageList.Add(item);
        UpdateItemCounterText(storageList.Count);
        item.GetComponent<ModelOfficer>().SelectTheModel(standsItemType);
        int index = storageList.Count-1;
        Transform standPosition = itemPositions.GetChild(index);
        item.SetParent(standPosition);
        ParticleSystem particle = particles.GetChild(index).GetComponent<ParticleSystem>();
        item.GetComponent<ItemActor>().itemMoveOfficer.ItemGoStand(standPosition, particle);
        
    }

    public Transform SellItem(CustomerBuyOfficer customerBuyOfficer)
    {
        if (storageList.Count > 0)
        {
            Transform itemToSell = storageList[storageList.Count - 1];
            storageList.Remove(itemToSell);
            UpdateItemCounterText(storageList.Count);
            emptySlotAmount = capacity - storageList.Count;
            itemStandActor.belongingRoom.roomStuffOrganizeOfficer.ItemStandNeedsRefill(this);
            return itemToSell;
        }
        else
        {
            return null;
        }
        
    }

    void UpdateItemCounterText(int count) // Also update the itemCount at RoomData
    {
        //itemCounterText.text = count.ToString();
        itemCounterText.text = count.ToString() + "/" + capacity.ToString();
        itemStandActor.belongingRoom.roomDataOfficer.roomActiveItemStands[itemStandActor.itemStandIndexForTheRoom] = count;
    }

    void SetTheIconAccordingToTheItemType()
    {
        foreach (Transform icon in boardIcons)
        {
            icon.gameObject.SetActive(false);
        }
        int selectedIconIndex = (int)standsItemType-1;
        boardIcons.GetChild(selectedIconIndex).gameObject.SetActive(true);
    }

    public void AddItemsToStandFromScript(int addAmount)
    {
        //int amountOfItemsToCreate = itemStandActor.belongingRoom.roomDataOfficer.roomActiveItemStands[itemStandActor.itemStandIndexForTheRoom];
        int amountOfItemsToCreate = addAmount;
        List <Transform> itemToPlaceList = itemStandActor.belongingRoom.roomFixturesOfficer.depotTruckPoint.GetComponent<DepotTruckPointActor>().wareHouseOfficer.GetItemsFromThePool(amountOfItemsToCreate);
        for (int i = 0; i < amountOfItemsToCreate; i++)
        {
            Transform item = itemToPlaceList[i];
            storageList.Add(item);
            UpdateItemCounterText(storageList.Count);
            item.GetComponent<ModelOfficer>().SelectTheModel(standsItemType);
            int index = storageList.Count - 1;
            Transform standPosition = itemPositions.GetChild(index);
            item.SetParent(standPosition);
            ParticleSystem particle = particles.GetChild(index).GetComponent<ParticleSystem>();
            particle.Play();
            item.position = standPosition.position;
            item.eulerAngles = standPosition.eulerAngles;
        }
    }

    public void AssignItemPositions(Transform _itemPositions) 
    {
        itemPositions = _itemPositions;
    }
    public void AssignParticleContainer(Transform _particleContainer)
    {
        particles = _particleContainer;
    }
}
