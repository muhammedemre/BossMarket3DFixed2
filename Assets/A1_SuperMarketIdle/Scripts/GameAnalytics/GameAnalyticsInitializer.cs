using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GameAnalyticsInitializer : MonoBehaviour, IGameAnalyticsATTListener
{
    public static GameAnalyticsInitializer instance;

    private void Awake()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    void Start()
    {
        IphoneAnalyticsPermission();
        //StartCoroutine(DelayerForTest());
    }

    void IphoneAnalyticsPermission()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GameAnalytics.RequestTrackingAuthorization(this);
        }
        else
        {
            GameAnalytics.Initialize();
            //StartCoroutine(DelayerForTest());
        }
    }

    IEnumerator DelayerForTest()
    {
        yield return new WaitForSeconds(5f);
        //IphoneAnalyticsPermission();
        print("INIT FROM DELAYER");
        GameAnalytics.Initialize();
    }

    public void GameAnalyticsATTListenerNotDetermined()
    {
        GameAnalytics.Initialize();
        //StartCoroutine(DelayerForTest());
    }
    public void GameAnalyticsATTListenerRestricted()
    {
        GameAnalytics.Initialize();
        //StartCoroutine(DelayerForTest());
    }
    public void GameAnalyticsATTListenerDenied()
    {
        GameAnalytics.Initialize();
        //StartCoroutine(DelayerForTest());
    }
    public void GameAnalyticsATTListenerAuthorized()
    {
        GameAnalytics.Initialize();
        //StartCoroutine(DelayerForTest());
    }

}
