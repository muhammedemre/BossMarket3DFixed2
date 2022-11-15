using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Sirenix.OdinInspector;

public class TruckClockActor : MonoBehaviour
{
    [SerializeField] RoomActor roomActor;
    [SerializeField] Slider slider;
    [SerializeField] List<float> truckTravelDuration = new List<float>() {5,3,2,1 };
    //public int currentLevel = 0;

    public void StartClock()
    {
        transform.GetChild(0).gameObject.SetActive(true);
        slider.value = 0;
        int truckSpeedLevel = roomActor.roomDataOfficer.truckSpeedLevel;       
        float duration = truckTravelDuration[truckSpeedLevel];
        //    DOTween.To(() => slider.value, x => myFloat = x, 52, 1);
        slider.DOValue(1, duration).OnComplete(()=> Disappear());
    }

    void Disappear()
    {
        transform.GetChild(0).gameObject.SetActive(false);
    }

    #region Button

    [Title("Run Clock")]
    [Button("Run Clock", ButtonSizes.Large)]
    void ButtonRunClock(int selectedModelIndex)
    {
        StartClock();
    }
    #endregion
}
