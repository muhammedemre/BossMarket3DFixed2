using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AdsManager : Manager
{
    public static AdsManager instance;
    public AdsActor adsActor;

    private void Awake()
    {
        StaticCheck();
    }

    void StaticCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }
}
