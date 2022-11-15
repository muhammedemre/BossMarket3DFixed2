using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class TutorialActor : SerializedMonoBehaviour
{
    [SerializeField] GameObject tutorialActor;
    [SerializeField] float tutorialCheckFrequency = 1f;
    float nextTutorialCheck = 0f;

    [SerializeField] Dictionary<TutorialState, GameObject> arrowDict = new Dictionary<TutorialState, GameObject>();

    [SerializeField] int currentStateIndex = 0;
    [SerializeField] ItemStandActor itemStandToFill, itemStandToActivate;
    [SerializeField] UpgradePointInteractionOfficer upgradePointInteraction;
    int previousItemAmount = 5555, previousMoneyAmount = 5555;
    int arrowIndex = 0;

    enum TutorialState
    {
        getItem, fillItemStand, collectMoney, openANewItemStand, upgrade
    }

    private void Update()
    {
        TutorialCheck();
    }

    void TutorialCheck()
    {
        if (nextTutorialCheck < Time.time)
        {
            nextTutorialCheck = Time.time + tutorialCheckFrequency;
            TutorialNeedCheck();
            StateCheck();
        }
    }

    void StateCheck()
    {
        switch (currentStateIndex)
        {
            case 0:           
                arrowIndex = 0;
                GetItemCheck();
                break;
            case 1:
                arrowIndex = 1;
                FillItemCheck();
                break;
            case 2:
                arrowIndex = 2;
                CollectMoneyCheck();
                break;
            case 3:
                arrowIndex = 3;
                OpenItemStandCheck();
                break;
            case 4:
                arrowIndex = 4;
                UpgradeCheck();
                break;
        }
        ActivateArrow(arrowIndex);
    }

    void GetItemCheck()
    {
        if (PlayerManager.instance.playerActor != null)
        {
            if (PlayerManager.instance.playerActor.itemCarryStackOfficer.carryingItemsList.Count > 0)
            {
                currentStateIndex++; 
            }
            
        }
        
    }

    void FillItemCheck()
    {
        if (itemStandToFill.itemStandItemHandleOfficer.storageList.Count > previousItemAmount)
        {
            currentStateIndex++;
        }
        previousItemAmount = itemStandToFill.itemStandItemHandleOfficer.storageList.Count;
    }

    void CollectMoneyCheck()
    {
        if (PlayerManager.instance.playerCurrencyOfficer.Money > previousMoneyAmount)
        {
            currentStateIndex++;
        }
        previousMoneyAmount = PlayerManager.instance.playerCurrencyOfficer.Money;
    }

    void OpenItemStandCheck()
    {
        if (itemStandToActivate.gameObject.activeSelf)
        {
            currentStateIndex++;
        }
    }

    void UpgradeCheck()
    {
        if (upgradePointInteraction.visited)
        {
            currentStateIndex++;
        }
    }

    void DeactivateArrows()
    {
        foreach (GameObject arrow in arrowDict.Values)
        {
            arrow.SetActive(false);
        }
    }

    void ActivateArrow(int arrowIndex)
    {
        TutorialState tempState = (TutorialState)arrowIndex;
        if (!arrowDict[tempState].activeSelf)
        {
            DeactivateArrows();
            arrowDict[tempState].SetActive(true);
        }
    }

    void TutorialNeedCheck()
    {
        if (DataManager.instance.tutorialFinished || currentStateIndex > arrowDict.Count-1)
        {
            DataManager.instance.tutorialFinished = true;
            Destroy(tutorialActor);
        }
    }
}
