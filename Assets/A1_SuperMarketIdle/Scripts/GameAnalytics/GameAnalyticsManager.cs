using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class GAATTListener : IGameAnalyticsATTListener
{
    public void GameAnalyticsATTListenerAuthorized()
    {
        GameAnalytics.Initialize();
    }

    public void GameAnalyticsATTListenerDenied()
    {
        GameAnalytics.Initialize();
    }

    public void GameAnalyticsATTListenerNotDetermined()
    {
        GameAnalytics.Initialize();
    }

    public void GameAnalyticsATTListenerRestricted()
    {
        GameAnalytics.Initialize();
    }
}

public class GameAnalyticsManager : MonoBehaviour
{
    public static GameAnalyticsManager instance;

    private void Start()
    {
        SingletonCheck();
        Init();
    }

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }

        instance = this;
    }

    void Init()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            GameAnalytics.RequestTrackingAuthorization(new GAATTListener());
        }
        else
        {
            GameAnalytics.Initialize();
        }
    }
}
