using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class BoostAdsWindowTimerOfficer : MonoBehaviour
{
    public BoostAdsWindowActor boostAdsWindowActor;
    public BoostAdsType boostAdsType;
    public Image progressImage;
    public TextMeshProUGUI durationText;

    public void StartTimer(int duration)
    {
        // StopCoroutine(LevelManager.instance.levelPowerUpOfficer.lastDeactiveAdsBoostCoroutine);

        ChangeDurationText(duration);
        StartCoroutine(TimeCounter(duration));
    }

    IEnumerator TimeCounter(int duration)
    {
        int timer = duration;
        DOTween.To(value => progressImage.fillAmount = value, 1f, 0f, duration);
        while (timer > 0)
        {
            timer--;
            ChangeDurationText(timer);
            yield return new WaitForSeconds(1);
        }

        boostAdsWindowActor.Revive(boostAdsType);
    }

    private void ChangeDurationText(int seconds)
    {
        TimeSpan time = TimeSpan.FromSeconds(seconds);
        durationText.text = time.ToString(@"mm\:ss");
    }
}