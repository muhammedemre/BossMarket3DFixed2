using System;
using UnityEngine;
using TMPro;
using System.Collections;

public class UIManager : Manager
{
    public static UIManager instance;
    public UITaskOfficers UITaskOfficers;
    public GameObject menu, inGameScreen, InGameMoney, InGameMoneyImage, settingsWindow, upgradeWindow, truckUpgradeWindow, dragToMoveScreen, splashScreenVideo;
    public AdsRewardPopUpActor adsRewardPopUpWindow;
    public Canvas mainCanvas;
    public SettingsMenuActor settingsMenuActor;
    [SerializeField] TextMeshProUGUI moneyText;
    public float splashVideoDuration;

    public bool noMoveUIOn = false;
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

    public override void GameStartProcess()
    {
        ActivateInGameScreen();
        StartCoroutine(AfterSplashVideo());
    }

    public void ActivateInGameScreen()
    {
        menu.gameObject.SetActive(false);
        inGameScreen.gameObject.SetActive(true);
    }

    public void ActivateMenuScreen()
    {
        menu.gameObject.SetActive(true);
        inGameScreen.gameObject.SetActive(false);
    }

    public void UpdateMoneyText(int money)
    {
        moneyText.text = money.ToString();
    }

    public void DeactivateDragToMove()
    {
        dragToMoveScreen.SetActive(false);
        ActivateInGameScreen();
    }

    IEnumerator AfterSplashVideo()
    {
        yield return new WaitForSeconds(splashVideoDuration);
        splashScreenVideo.SetActive(false);
        GameManager.instance.gameManagerObserverOfficer.Publish(ObserverSubjects.Menu);
    }
}
