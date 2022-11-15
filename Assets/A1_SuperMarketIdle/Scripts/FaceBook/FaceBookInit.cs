using System;
using Facebook.Unity;
using GameAnalyticsSDK;
using UnityEngine;

namespace DefaultNamespace
{
    public class FaceBookInit : MonoBehaviour
    {
        private void Awake()
        {
            if (FindObjectsOfType<FaceBookInit>().Length > 1)
            {
                Destroy(gameObject);
                return;
            }
            DontDestroyOnLoad(transform.gameObject);

            if (Application.platform == RuntimePlatform.IPhonePlayer)
            {
                Application.targetFrameRate = 60;
            }

            FB.Init(FBInitCallback);
            GameAnalytics.Initialize();
        }

        private void FBInitCallback()
        {
            if (FB.IsInitialized)
            {
                FB.ActivateApp();
            }
        }

        public void OnApplicationPause(bool pauseStatus)
        {
            if (!pauseStatus)
            {
                if (FB.IsInitialized)
                {
                    FB.ActivateApp();
                }
            }
        }
    }
}
