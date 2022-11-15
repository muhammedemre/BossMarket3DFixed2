using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ItemCarryStackOfficer : MonoBehaviour
{
    public int carryCapacity;
    public List<Transform> carryingItemsList = new List<Transform>();
    [SerializeField] Transform carryPoints;
    [SerializeField] float itemPaddingDistance;
    [SerializeField] PlayerAnimationOfficer playerAnimationOfficer;
    [SerializeField] float heightEffectRate;
    [SerializeField] StuffActor stuffActor;

    [SerializeField] float takeItemFromTheStackFrequency;
    float nextTakeItemFromTheStackTime;

    [SerializeField] AudioSource audioSource;

    public void RegisterToTheItemTakeLine(ItemTakePlaceStackOfficer itemTakePlaceStackOfficer)
    {
        itemTakePlaceStackOfficer.StuffLineAdd(this);
    }
    public void UnsubscribeToTheItemTakeLine(ItemTakePlaceStackOfficer itemTakePlaceStackOfficer)
    {
        itemTakePlaceStackOfficer.StuffLineRemove(this);
    }
    public void AddAnItemToStack(Transform item)
    {
        carryingItemsList.Add(item);
        float heightEffect = Mathf.Pow(heightEffectRate, carryingItemsList.Count);

        bool playerStack = this.tag == "Stuff"? false : true;
        if (carryingItemsList.Count == 1)
        {
            
            item.SetParent(LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().itemTempContainer);
            item.GetComponent<ItemActor>().itemMoveOfficer.FollowTheStackTarget(carryPoints, heightEffect, carryingItemsList.Count, playerStack);
            
        }
        else
        {
            item.SetParent(LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().itemTempContainer);
            item.GetComponent<ItemActor>().itemMoveOfficer.FollowTheStackTarget(carryingItemsList[carryingItemsList.Count - 2], heightEffect, carryingItemsList.Count, playerStack);
        }
        //if (this.tag == "Player")
        //{
        //    if (UIManager.instance.settingsMenuActor.soundState)
        //    {
        //        audioSource.Play();
        //    }
        //    VibrationManager.instance.PlaySoftVibration();
        //}
        
    }

    public void CheckToGiveItemToTheStand(ItemStandActor itemStandActor, bool isStuff)
    {
        if (nextTakeItemFromTheStackTime < Time.time)
        {
            GiveTheItem(itemStandActor, isStuff);
            nextTakeItemFromTheStackTime = Time.time + takeItemFromTheStackFrequency;
        }
    }

    void GiveTheItem(ItemStandActor itemStandActor, bool isStuff)
    {
        ItemStandItemHandleOfficer itemStandItemHandleOfficer = itemStandActor.itemStandItemHandleOfficer;
        if (carryingItemsList.Count > 0)
        {
            bool canStandTake = CheckStandCapacity(itemStandItemHandleOfficer);
            if (canStandTake)
            {
                Transform itemToGive = TakeItemFromTheStack();
                itemToGive.GetComponent<ItemActor>().itemMoveOfficer.stackTarget = null;
                carryingItemsList.Remove(itemToGive);
                itemStandItemHandleOfficer.AddAnItemToStand(itemToGive);
                if (isStuff && carryingItemsList.Count == 0)
                {
                    stuffActor.stuffAIOfficer.TakeAnItem();
                }
            }
            else 
            {
                itemStandItemHandleOfficer.itemStandActor.belongingRoom.roomStuffOrganizeOfficer.RemoveItemStandFromInNeedList(itemStandItemHandleOfficer);
                if (isStuff)
                {
                    stuffActor.stuffAIOfficer.roomInIt.roomStuffOrganizeOfficer.RegisterToReadyToServe(stuffActor);
                }                   
            }
        }      
    }

    public Transform TakeItemFromTheStack()
    {
        int lastItemIndex = carryingItemsList.Count - 1;
        return carryingItemsList[lastItemIndex];
    }

    bool CheckStandCapacity(ItemStandItemHandleOfficer itemStandItemHandleOfficer)
    {
        return ((itemStandItemHandleOfficer.capacity > itemStandItemHandleOfficer.storageList.Count) ? true : false);
    }
}
