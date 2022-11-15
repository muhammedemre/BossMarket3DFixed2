using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UITaskOfficers : MonoBehaviour
{
    [SerializeField] AudioSource buttonAudio;

    public void SettingsButton(bool state)
    {
        UIManager.instance.settingsWindow.gameObject.SetActive(state);
        if (UIManager.instance.settingsMenuActor.soundState)
        {
            buttonAudio.Play();
        }
    }

    public void UpgradeWindowClose()
    {
        UIManager.instance.noMoveUIOn = false;
        UpgradeWindowSetState(false, null);
    }

    public void UpgradeWindowSetState(bool state, RoomActor relatedRoomActor)
    {
        UIManager.instance.noMoveUIOn = state;
        UIManager.instance.upgradeWindow.transform.GetChild(0).gameObject.SetActive(state);
        if (state)
        {
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowUpgradeOfficer>().relatedRoomActor = relatedRoomActor;
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowActor>().GetPrepared();
        }
    }

    public void TruckUpgradeWindowClose()
    {
        UIManager.instance.noMoveUIOn = false;
        OpenAndCloseTruckUpgradeWindow(false, null);
    }

    public void OpenAndCloseTruckUpgradeWindow(bool state, RoomActor relatedRoomActor)
    {
        UIManager.instance.noMoveUIOn = state;
        UIManager.instance.truckUpgradeWindow.SetActive(state);
        if (state)
        {
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowUpgradeOfficer>().relatedRoomActor = relatedRoomActor;
            UIManager.instance.upgradeWindow.GetComponent<UpgradeWindowActor>().GetPrepared();
        }
    }

    public void MoneyCheat()
    {
        PlayerManager.instance.playerCurrencyOfficer.Money += 9999;
    }

    public void ActiveAdsRewardPopUp(AdsRewardPopUpState adsRewardPopUpState)
    {
        AdsRewardPopUpActor adsRewardPopUpActor = UIManager.instance.adsRewardPopUpWindow;
        adsRewardPopUpActor.gameObject.SetActive(true);
        adsRewardPopUpActor.ActivePopUpState(adsRewardPopUpState);
    }

    public void DeactivateAdsRewardPopUp()
    {
        UIManager.instance.noMoveUIOn = false;
        UIManager.instance.adsRewardPopUpWindow.gameObject.SetActive(false);
    }
}
