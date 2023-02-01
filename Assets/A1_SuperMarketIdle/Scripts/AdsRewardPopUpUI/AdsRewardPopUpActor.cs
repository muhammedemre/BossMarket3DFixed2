using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Sirenix.OdinInspector;
using GameAnalyticsSDK;

public class AdsRewardPopUpActor : MonoBehaviour
{
    [SerializeField] private GameObject fillRoomPopUp, freeCoinsPopUp;

    [Header("Free Coins PopUp")]
    public TextMeshProUGUI freeCoinText;

    public void ActivePopUpState(AdsRewardPopUpState state)
    {
        UIManager.instance.noMoveUIOn = true;

        int reward = LevelManager.instance.levelPowerUpOfficer.CoinRewardCalculate();
        freeCoinText.text = $"{reward}$";

        if (state == AdsRewardPopUpState.FillRoom)
        {
            freeCoinsPopUp.SetActive(false);
            fillRoomPopUp.SetActive(true);
            GameAnalytics.NewDesignEvent("PopUpRewardAd_FillRoom");
        }
        else if (state == AdsRewardPopUpState.FreeCoins)
        {
            fillRoomPopUp.SetActive(false);
            freeCoinsPopUp.SetActive(true);
            GameAnalytics.NewDesignEvent("PopUpRewardAd_FreeCoins");
        }
    }
}

public enum AdsRewardPopUpState { FillRoom, FreeCoins }
