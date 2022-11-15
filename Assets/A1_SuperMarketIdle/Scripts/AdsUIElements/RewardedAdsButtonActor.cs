using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class RewardedAdsButtonActor : SerializedMonoBehaviour
{
    public Button button;
    public MyGoogleAdMob.AdPlacement placement;
    public UnityEvent OnReward = new UnityEvent();

    private bool isLoaded = false;

    private void OnEnable()
    {
        button.interactable = false;
        button.onClick.AddListener(OnClick);
    }

    private void OnDisable()
    {
        button.onClick.RemoveListener(OnClick);
    }

    private void Update()
    {
        CheckInteractable();
    }

    private void CheckInteractable()
    {
        isLoaded = AdsManager.instance.adsActor.adsShowOfficer.RewardedAds[placement].IsLoaded();
        button.interactable = isLoaded;
    }

    private void OnClick()
    {
        button.interactable = false;
        isLoaded = false;
        AdsManager.instance.adsActor.adsShowOfficer.ShowRewardedAd(placement, (_) =>
        {
            UIManager.instance.UITaskOfficers.DeactivateAdsRewardPopUp();
            OnReward?.Invoke();
        }, (errorMessage) =>
        {
            Debug.LogError(errorMessage);
        });
    }
}

