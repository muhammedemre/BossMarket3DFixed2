using System.Collections.Generic;
using DG.Tweening;
using Sirenix.OdinInspector;
using UnityEngine;

public class BoostAdsWindowActor : SerializedMonoBehaviour
{
    public Dictionary<BoostAdsType, BoostAd> boostAds;
    public float reviveDurationSeconds;

    private void Awake()
    {
        foreach (KeyValuePair<BoostAdsType, BoostAd> boostAd in boostAds)
        {
            boostAd.Value.timerOfficer.gameObject.SetActive(false);
            boostAd.Value.buttonActor.gameObject.SetActive(true);
        }
    }

    public void OpenTimerOfficer(BoostAdsType boostAdsType)
    {
        boostAds[boostAdsType].timerOfficer.transform.position = boostAds[boostAdsType].closeRect.transform.position;
        boostAds[boostAdsType].timerOfficer.gameObject.SetActive(true);
        boostAds[boostAdsType].timerOfficer.StartTimer(boostAds[boostAdsType].durationForSeconds);

        boostAds[boostAdsType].buttonActor.transform.DOMove(boostAds[boostAdsType].closeRect.transform.position, reviveDurationSeconds / 2f).OnComplete(() =>
        {
            boostAds[boostAdsType].buttonActor.gameObject.SetActive(false);

            boostAds[boostAdsType].timerOfficer.transform.DOMove(boostAds[boostAdsType].openRect.transform.position, reviveDurationSeconds / 2f);
        });
    }

    public void Revive(BoostAdsType boostAdsType)
    {
        boostAds[boostAdsType].timerOfficer.transform.DOMove(boostAds[boostAdsType].closeRect.transform.position, reviveDurationSeconds / 2f).OnComplete(() =>
        {
            boostAds[boostAdsType].timerOfficer.gameObject.SetActive(false);

            boostAds[boostAdsType].buttonActor.transform.position = boostAds[boostAdsType].closeRect.transform.position;
            boostAds[boostAdsType].buttonActor.gameObject.SetActive(true);
            boostAds[boostAdsType].buttonActor.transform.DOMove(boostAds[boostAdsType].openRect.transform.position, reviveDurationSeconds / 2f);
        });
    }
}

public class BoostAd
{
    public BoostAdsWindowButtonActor buttonActor;
    public BoostAdsWindowTimerOfficer timerOfficer;
    public RectTransform closeRect, openRect;
    public int durationForSeconds;
}
public enum BoostAdsType { Speed, Revenue, Cashier, FillShelves }