using System;
using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

namespace MyGoogleAdMob
{
    public class GoogleAdMobAdsDataOfficer : SerializedMonoBehaviour
    {
        public Dictionary<AdPlacement, GoogleAdMobType> Ads = new Dictionary<AdPlacement, GoogleAdMobType>();
    }

    [Serializable]
    public class GoogleAdMobType
    {
        public AdFormat adFormat;
        public string androidUnitID;
        public string iosUnitID;
        [ReadOnly] public bool isLoaded = false;
    }

    public enum AdFormat { Rewarded, Interstitial }
    public enum AdPlacement
    {
        TestRewarded,
        TestInterstitial,
        CoinsBoostForSomeTimeRW,
        CoinRewardRW,
        RoomExtend,
        RoomAdd,
        SpeedUpCustomer,
        ReFillTheRoomRW,
        WorkerUpgrade,
        Cashier,
        ItemStandOpen,
    }
}
