using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInteractionOfficer : MonoBehaviour
{
    [SerializeField] PlayerActor playerActor;
    [SerializeField] bool busyWCashier = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "CashierDesk")
        {
            if (UIManager.instance.settingsMenuActor.soundState)
            {
                other.GetComponent<RootFinderOfficer>().root.GetComponent<CashierActor>().moneyHandleOfficer.audioSource.Play();
            }
        }
        else if (other.CompareTag("PowerBoostBox"))
        {
            PowerBoostModelOfficer.PowerBoostType powerBoostType = other.GetComponent<RootFinderOfficer>().root.GetComponent<PowerBoostBoxActor>().selectedBoostType;

            LevelManager.instance.levelPowerUpOfficer.DestroyPreviousBoxes(LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>());
            if (powerBoostType == PowerBoostModelOfficer.PowerBoostType.Case)
            {
                UIManager.instance.UITaskOfficers.ActiveAdsRewardPopUp(AdsRewardPopUpState.FreeCoins);
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "CashierDesk")
        {
            busyWCashier = false;
        }
        else if (other.tag == "ItemTakePlace")
        {
            playerActor.itemCarryStackOfficer.UnsubscribeToTheItemTakeLine(other.GetComponent<ItemTakePlaceStackOfficer>());
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "ItemTakePlace")
        {
            playerActor.itemCarryStackOfficer.RegisterToTheItemTakeLine(other.GetComponent<ItemTakePlaceStackOfficer>());
        }
        else if (other.tag == "ItemStand")
        {
            playerActor.itemCarryStackOfficer.CheckToGiveItemToTheStand(other.GetComponent<ItemStandActor>(), false);
        }
        else if (other.tag == "CashierDesk")
        {
            //busyWCashier = true;
            if (!busyWCashier)
            {
                busyWCashier = true;
            }
            other.GetComponent<RootFinderOfficer>().root.GetComponent<CashierActor>().moneyHandleOfficer.GetTheMoneyOnTheDesk();
        }
    }
}
