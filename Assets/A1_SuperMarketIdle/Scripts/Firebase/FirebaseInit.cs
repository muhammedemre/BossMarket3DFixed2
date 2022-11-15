using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

public class FirebaseInit : MonoBehaviour
{
    private void Start()
    {
        //FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => { FirebaseAnalytics.SetAnalyticsCollectionEnabled(true); } );
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task => { var app = FirebaseApp.DefaultInstance; });
    }
}
