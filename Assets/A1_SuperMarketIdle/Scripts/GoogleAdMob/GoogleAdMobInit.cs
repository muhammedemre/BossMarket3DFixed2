using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using GoogleMobileAds.Api;
using System;
using UnityEngine.Events;
using GoogleMobileAds.Common;

namespace MyGoogleAdMob
{
    public class GoogleAdMobInit : MonoBehaviour
    {
        [HideInInspector] public UnityEvent OnInitialized = new UnityEvent();

        private void Awake()
        {
            if (FindObjectsOfType<GoogleAdMobInit>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(gameObject);
        }

        private void Start()
        {
            MobileAds.SetiOSAppPauseOnBackground(true);

            RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetSameAppKeyEnabled(true).build();
            MobileAds.SetRequestConfiguration(requestConfiguration);

            MobileAdsEventExecutor.Initialize();
            MobileAds.Initialize(HandleInitCompleteAction);
        }

        private void HandleInitCompleteAction(InitializationStatus initStatus)
        {
            Debug.Log("Mobile Ads On Complete");
            OnInitialized?.Invoke();
        }
    }
}
