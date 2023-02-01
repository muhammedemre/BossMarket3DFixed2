using System.Collections;
using System.Collections.Generic;
using GameAnalyticsSDK;
using UnityEngine;

public class GADelayer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(DelayerForTest());
    }

    IEnumerator DelayerForTest()
    {
        yield return new WaitForSeconds(15f);
        //IphoneAnalyticsPermission();
        //print("INIT FROM DELAYER");
        //GameAnalytics.Initialize();
    }
}
