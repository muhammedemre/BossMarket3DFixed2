using Firebase.Analytics;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventsManager : MonoBehaviour
{
    public static EventsManager instance;

    private void Awake()
    {
        SingletonCheck();
    }

    void SingletonCheck()
    {
        if (instance != null)
        {
            Destroy(this);
        }
        instance = this;
    }

    public void RoomEventsTrigger(bool isRoomAdd, int roomIndex)
    {
        if (isRoomAdd)
        {
            FirebaseAnalytics.LogEvent("Room_events", new Parameter("Room_Add", roomIndex.ToString()));
        }
        else
        {
            FirebaseAnalytics.LogEvent("Room_events", new Parameter("Room_Extension", roomIndex.ToString()));
        }       
    }

    public void AddsTrigger(string parameterType, int roomIndex)
    {
        FirebaseAnalytics.LogEvent("Ad_events", new Parameter(parameterType, roomIndex.ToString()));
    }
}
