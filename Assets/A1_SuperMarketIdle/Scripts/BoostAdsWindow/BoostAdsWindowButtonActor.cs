using System.Collections;
using System.Collections.Generic;
using DG.Tweening;
using UnityEngine;

public class BoostAdsWindowButtonActor : MonoBehaviour
{
    public BoostAdsWindowActor boostAdsWindowActor;
    public BoostAdsType boostAdsType;

    public void OnRewardedButton()
    {
        boostAdsWindowActor.OpenTimerOfficer(boostAdsType);
    }
}
