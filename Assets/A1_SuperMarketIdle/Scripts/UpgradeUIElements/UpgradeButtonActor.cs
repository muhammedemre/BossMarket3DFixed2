using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Sirenix.OdinInspector;
using TMPro;

public class UpgradeButtonActor : MonoBehaviour
{
    [SerializeField] UpgradeWindowActor upgradeWindowActor;
    [SerializeField] GameObject moneyAmountText, upgradeButtonLabelText, moneyLabel, moneyUI, buttonLevelText, upgradeLevel0, upgradeComplete, upgradeOtherLevels;
    [SerializeField] List<Sprite> buttonBGSpriteList = new List<Sprite>();
    [SerializeField] List<string> buttonLabelTextList = new List<string>();
    [SerializeField] ButtonType buttonType;
    [SerializeField] int buttonIndex;
    [SerializeField] Button button;

    [SerializeField] int buttonLevel = 0;
    [SerializeField] AudioSource audioSource;

    enum ButtonType
    {
        PlayerUpgrade, WorkerUpgrade, EnvironmentUpgrade, TruckUpgrade
    }

    delegate void ButtonFunctions(int buttonIndex);
    event ButtonFunctions buttonFunctions;

    private void Start()
    {
        switch (buttonType)
        {
            case ButtonType.PlayerUpgrade:
                buttonFunctions += UpgradePlayer;
                break;
            case ButtonType.WorkerUpgrade:
                buttonFunctions += UpgradeWorker;
                break;
            case ButtonType.EnvironmentUpgrade:
                buttonFunctions += UpgradeEnvironment;
                break;
            case ButtonType.TruckUpgrade:
                buttonFunctions += UpgradeTruck;
                break;
        }
        PrepareTheButton();
    }

    public void UpgradeButtonProcess()
    {
        buttonFunctions(buttonIndex);
        //audioSource.PlayOneShot(audioSource.clip);
        if (UIManager.instance.settingsMenuActor.soundState)
        {
            audioSource.Play();
        }
    }

    public void PrepareTheButton()
    {
        bool fullyUpgraded = buttonLevel == 4 ? true : false;
        if (!fullyUpgraded)
        {
            int newRequiredMoney = Cost();
            moneyAmountText.GetComponent<TextMeshProUGUI>().text = newRequiredMoney.ToString();
        }

        SetButtonState(fullyUpgraded);
    }


    void SetButtonLabel(int buttonLabelTextIndex)
    {
        upgradeButtonLabelText.GetComponent<TextMeshProUGUI>().text = buttonLabelTextList[buttonLabelTextIndex];
    }

    void SetButtonState(bool fullyUpgraded)
    {
        if (fullyUpgraded)
        {
            FullyUpgradedProcess();

            ButtonLevelInfoText(buttonLevel);
        }
        else
        {
            int spriteAndLabelIndex = 1;
            if (buttonType == ButtonType.WorkerUpgrade)
            {
                spriteAndLabelIndex = (buttonLevel == 0) ? 0 : 1;

                ButtonLevelInfoText(buttonLevel);
            }
            else
            {
                ButtonLevelInfoText(buttonLevel);
            }
            NotFullyUpgradedProcess(spriteAndLabelIndex);
        }
    }

    void ButtonLevelInfoText(int _buttonLevel)
    {
        int butLevel = _buttonLevel < 0 ? 0 : _buttonLevel;
        buttonLevelText.GetComponent<TextMeshProUGUI>().text = "Level " + butLevel.ToString();
    }

    void UpgradePlayer(int _buttonIndex)
    {
        bool successFullButtonPush = buttonLevel < DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count - 1;
        if (successFullButtonPush)
        {
            bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel]);
            if (playerHasEnoughMoney)
            {
                PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
                upgradeWindowActor.upgradeWindowUpgradeOfficer.PlayerUpgradeButton(_buttonIndex);
                buttonLevel++;
                int newCost = DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
                PrepareTheButton();
            }
            else
            {
                // money is not enough to use the button
            }
        }
        else if (buttonLevel == DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count - 1)//Button max level
        {
            bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel]);
            if (playerHasEnoughMoney)
            {
                PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
                upgradeWindowActor.upgradeWindowUpgradeOfficer.PlayerUpgradeButton(_buttonIndex);
                buttonLevel++;
                SetButtonState(true);
            }

        }
    }

    void UpgradeWorker(int _buttonIndex)
    {
        bool successFullButtonPush = buttonLevel < DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts.Count - 1;
        if (successFullButtonPush)
        {
            if (buttonLevel == 0)
            {
                bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts[buttonLevel]);
                if (playerHasEnoughMoney)
                {
                    PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts[buttonLevel];
                    UpgradeWorkerSuccess(_buttonIndex);
                }
                else
                {
                    // money is not enough to use the button
                }
            }
            else
            {
                AdsManager.instance.adsActor.adsShowOfficer.ShowRewardedAd(MyGoogleAdMob.AdPlacement.WorkerUpgrade, (_) =>
                {
                    UpgradeWorkerSuccess(_buttonIndex);
                }, (errorMessage) =>
                {
                    Debug.LogError(errorMessage);
                });
            }
        }
        else if (buttonLevel == DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count - 1)//Button max level
        {
            AdsManager.instance.adsActor.adsShowOfficer.ShowRewardedAd(MyGoogleAdMob.AdPlacement.WorkerUpgrade, (_) =>
                {
                    UpgradeWorkerSuccess(_buttonIndex);
                }, (errorMessage) =>
                {
                    Debug.LogError(errorMessage);
                });

            // bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel]);
            // if (playerHasEnoughMoney)
            // {
            //     PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
            //     upgradeWindowActor.upgradeWindowUpgradeOfficer.WorkerUpgradeButton(_buttonIndex);
            //     buttonLevel++;
            //     SetButtonState(true);
            // }
        }
    }

    void UpgradeWorkerSuccess(int _buttonIndex)
    {
        upgradeWindowActor.upgradeWindowUpgradeOfficer.WorkerUpgradeButton(_buttonIndex);
        buttonLevel++;
        if (buttonLevel < DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts.Count - 1)
        {
            int newCost = DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts[buttonLevel];
        }
        PrepareTheButton();
    }

    void UpgradeTruck(int _buttonIndex)
    {
        bool successFullButtonPush = buttonLevel < DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts.Count - 1;
        if (successFullButtonPush)
        {
            bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts[buttonLevel]);
            if (playerHasEnoughMoney)
            {
                PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts[buttonLevel];
                upgradeWindowActor.upgradeWindowUpgradeOfficer.TruckUpgradeButton(_buttonIndex);
                buttonLevel++;
                int newCost = DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts[buttonLevel];
                PrepareTheButton();
            }
            else
            {
                // money is not enough to use the button
            }
        }
        else if (buttonLevel == DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count - 1)//Button max level
        {
            bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel]);
            if (playerHasEnoughMoney)
            {
                PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
                upgradeWindowActor.upgradeWindowUpgradeOfficer.TruckUpgradeButton(_buttonIndex);
                buttonLevel++;
                SetButtonState(true);
            }

        }
    }

    void UpgradeEnvironment(int _buttonIndex)
    {
        //bool successFullButtonPush = buttonLevel < DataManager.instance.gameVariablesData.EnvironmentButtonCosts.Count - 1;
        //if (successFullButtonPush)
        //{
        //    bool playerHasEnoughMoney = CheckIfPlayerHasEnoughMoney(DataManager.instance.gameVariablesData.EnvironmentButtonCosts[buttonLevel]);
        //    if (playerHasEnoughMoney)
        //    {
        //        PlayerManager.instance.playerCurrencyOfficer.Money -= DataManager.instance.gameVariablesData.EnvironmentButtonCosts[buttonLevel];
        //        upgradeWindowActor.upgradeWindowUpgradeOfficer.EnvironmentUpgradeButton(_buttonIndex);
        //        buttonLevel++;
        //        int newCost = DataManager.instance.gameVariablesData.EnvironmentButtonCosts[buttonLevel];
        //        PrepareTheButton();
        //    }
        //    else
        //    {
        //        // money is not enough to use the button
        //    }
        //}
        //else if(buttonLevel == DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts.Count - 1)//Button max level
        //{
        //    upgradeWindowActor.upgradeWindowUpgradeOfficer.EnvironmentUpgradeButton(_buttonIndex);
        //    buttonLevel++;
        //    SetButtonState(true);
        //}
    }


    bool CheckIfPlayerHasEnoughMoney(int buttonCost)
    {
        bool hasEnoughMoney = (PlayerManager.instance.playerCurrencyOfficer.Money >= buttonCost) ? true : false;
        return hasEnoughMoney;
    }


    public void UpdateTheButton(int _buttonLevel)
    {
        buttonLevel = _buttonLevel;
        PrepareTheButton();
    }

    void FullyUpgradedProcess()
    {
        moneyUI.SetActive(false);
        moneyAmountText.SetActive(false);
        upgradeButtonLabelText.SetActive(false);
        upgradeLevel0.SetActive(false);
        upgradeOtherLevels.SetActive(false);
        upgradeComplete.SetActive(true);
        Sprite selectedSprite = buttonBGSpriteList[2];
        moneyLabel.GetComponent<Image>().sprite = selectedSprite;
        button.interactable = false;
    }

    void NotFullyUpgradedProcess(int spriteAndLabelIndex)
    {
        Sprite selectedSprite = buttonBGSpriteList[spriteAndLabelIndex];
        moneyLabel.GetComponent<Image>().sprite = selectedSprite;
        button.interactable = true;
        if (buttonType == ButtonType.WorkerUpgrade)
        {
            upgradeLevel0.SetActive(buttonLevel == 0);
            upgradeOtherLevels.SetActive(buttonLevel != 0);
        }
        else
        {
            upgradeLevel0.SetActive(true);
        }
        upgradeComplete.SetActive(false);
        SetButtonLabel(spriteAndLabelIndex);
        moneyUI.SetActive(true);
        moneyAmountText.SetActive(true);
        if (buttonType != ButtonType.TruckUpgrade)
        {
            upgradeButtonLabelText.SetActive(true);
        }

    }

    int Cost()
    {
        int value = 0;
        switch (buttonType)
        {
            case ButtonType.PlayerUpgrade:
                value = DataManager.instance.gameVariablesData.PlayerUpgradeButtonCosts[buttonLevel];
                break;
            case ButtonType.WorkerUpgrade:
                value = DataManager.instance.gameVariablesData.WorkerUpgradeButtonCosts[buttonLevel];
                break;
            case ButtonType.EnvironmentUpgrade:
                value = DataManager.instance.gameVariablesData.EnvironmentButtonCosts[buttonLevel];
                break;
            case ButtonType.TruckUpgrade:
                value = DataManager.instance.gameVariablesData.TruckUpgradeButtonCosts[buttonLevel];
                break;
        }
        return value;
    }

    //#region Button

    //[Title("Set Button State")]
    //[Button("Set Button State", ButtonSizes.Large)]
    //void ButtonSetButtonState()
    //{
    //    SetButtonState();
    //}
    //#endregion
}
