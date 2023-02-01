using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using GameAnalyticsSDK;
using UnityEngine;

public class BoostAdsWindowButtonActor : MonoBehaviour
{
    public BoostAdsWindowActor boostAdsWindowActor;
    public BoostAdsType boostAdsType;

    public void OnRewardedButton()
    {
        GameAnalytics.NewDesignEvent("BoostAds_" + boostAdsType.ToString());
        boostAdsWindowActor.OpenTimerOfficer(boostAdsType);
    }
}
