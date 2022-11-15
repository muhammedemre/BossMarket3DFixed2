using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelPowerUpOfficer : MonoBehaviour
{
    public bool speedUpActive = false, coinBoostActive = false;
    public float coinBoostCoefficient = 1, speedUpCoefficient = 1, coinRewardProcent, cashierLiveDuration;
    [SerializeField] int minCoinReward;
    float normalCustomerSpeed;
    [SerializeField] GameObject powerBoostBoxPrefab;

    [SerializeField] float powerBoostBoxFrequency;
    [SerializeField] float adsBoostFrequency, adsBoostDeactiveDuration;
    float nextPowerBoostBoxAdditionTime;

    private void Update()
    {
        PowerBoostAddCheck();
    }

    public void FillTheRoomsItemStands()
    {

        RoomActor theRoomPlayerIsIn = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.FindTheRoomThatPlayerIn();
        foreach (Transform itemStand in theRoomPlayerIsIn.roomFixturesOfficer.roomItemStands)
        {
            ItemStandActor itemStandActor = itemStand.GetComponent<ItemStandActor>();
            int neededAmountToGetfull = itemStandActor.itemStandItemHandleOfficer.capacity - itemStandActor.itemStandItemHandleOfficer.storageList.Count;
            itemStandActor.itemStandItemHandleOfficer.AddItemsToStandFromScript(neededAmountToGetfull);
        }
    }

    public void SpeedUp2xTheCustomersForSomeTime(float seconds)
    {
        SpeedUpTheCustomersForSomeTime(2, seconds);
    }

    public void SpeedUpTheCustomersForSomeTime(float speedBoostCoefficient, float duration)
    {
        ChangeTheSpeedOfCustomers(true, speedBoostCoefficient);
        StartCoroutine(DeactivateSpeedUpTheCustomers(duration));
    }

    public void CoinRewardCalculateAndTrigger()
    {
        int rewardAmount = CoinRewardCalculate();

        GetCoinReward(rewardAmount);
    }

    public int CoinRewardCalculate()
    {
        int rewardAmount = (int)(PlayerManager.instance.playerCurrencyOfficer.investedMoneyAmount * coinRewardProcent);
        rewardAmount = rewardAmount < minCoinReward ? minCoinReward : rewardAmount;
        return rewardAmount;
    }
    public void GetCoinReward(int coinAmount)
    {
        PlayerManager.instance.playerCurrencyOfficer.MoneyDepositToTheWallet(coinAmount);
    }

    public void Coin2XBoostForSomeTime(float seconds)
    {
        CoinEarnBoostForSomeTime(2, seconds);
    }

    public void CoinEarnBoostForSomeTime(float _coinBoostCoefficient, float duration)
    {
        coinBoostActive = true;
        coinBoostCoefficient = _coinBoostCoefficient;
        StartCoroutine(DeactivateCoinBoost(duration));
    }

    IEnumerator DeactivateCoinBoost(float duration)
    {
        yield return new WaitForSeconds(duration);
        coinBoostActive = false;
        coinBoostCoefficient = 1;
    }

    IEnumerator DeactivateSpeedUpTheCustomers(float duration)
    {
        yield return new WaitForSeconds(duration);
        speedUpActive = false;
        ChangeTheSpeedOfCustomers(false, 1);
    }

    void ChangeTheSpeedOfCustomers(bool boost, float speedCoefficient)
    {
        if (boost)
        {
            foreach (CustomerActor customer in LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.activeCustomersInLevel)
            {
                normalCustomerSpeed = customer.customerMoveOfficer.customer.speed;
                float boostedSpeed = normalCustomerSpeed * speedCoefficient;
                customer.customerMoveOfficer.SetTheCustomerSpeed(boostedSpeed);
            }
        }
        else
        {
            foreach (CustomerActor customer in LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.activeCustomersInLevel)
            {
                float boostedSpeed = normalCustomerSpeed * speedCoefficient;
                customer.customerMoveOfficer.SetTheCustomerSpeed(boostedSpeed);
            }
        }

    }

    void RandomlyPlaceAPowerBoostBox()
    {

        LevelActor currentLevelActor = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>();
        DestroyPreviousBoxes(currentLevelActor);

        int randomIndexForTheRoom = Random.Range(0, currentLevelActor.levelDataOfficer.activeRooms.Count);
        GameObject randomRoomToPlaceTheBox = currentLevelActor.levelDataOfficer.activeRooms[randomIndexForTheRoom];

        int randomIndexForBoxPlace = Random.Range(0, randomRoomToPlaceTheBox.GetComponent<RoomActor>().powerBoostPlacementPositions.childCount);
        Transform boxPosition = randomRoomToPlaceTheBox.GetComponent<RoomActor>().powerBoostPlacementPositions.GetChild(randomIndexForBoxPlace);

        GameObject tempPowerBoostBox = Instantiate(powerBoostBoxPrefab, boxPosition.position, Quaternion.identity, boxPosition);
        tempPowerBoostBox.GetComponent<PowerBoostBoxActor>().powerBoostModelOfficer.SelectARandomModel();
    }

    public void DestroyPreviousBoxes(LevelActor currentLevelActor) // if not looted yet.
    {
        foreach (GameObject room in currentLevelActor.levelDataOfficer.activeRooms.Values)
        {
            foreach (Transform position in room.GetComponent<RoomActor>().powerBoostPlacementPositions)
            {
                if (position.childCount > 0)
                {
                    Destroy(position.GetChild(0).gameObject);
                }
            }
        }
    }

    void PowerBoostAddCheck()
    {
        if (nextPowerBoostBoxAdditionTime < Time.time)
        {
            nextPowerBoostBoxAdditionTime = Time.time + powerBoostBoxFrequency;
            RandomlyPlaceAPowerBoostBox();
        }
    }

    // public Coroutine lastNewAdsBoostCoroutine;
    // public Coroutine lastDeactiveAdsBoostCoroutine;
    // public void NewAdsBoostCheck()
    // {
    //     if (lastNewAdsBoostCoroutine != null) StopCoroutine(lastNewAdsBoostCoroutine);
    //     lastNewAdsBoostCoroutine = StartCoroutine(AdsBoostCheck());
    //     lastDeactiveAdsBoostCoroutine = StartCoroutine(DeactiveBoostAdsWindowCheck());
    // }

    // IEnumerator AdsBoostCheck()
    // {
    //     yield return new WaitForSeconds(adsBoostFrequency);
    //     RandomlyBoostAdsWindow();
    // }

    // IEnumerator DeactiveBoostAdsWindowCheck()
    // {
    //     yield return new WaitForSeconds(adsBoostDeactiveDuration);
    //     UIManager.instance.boostAdsWindowActor.DeactiveState(false, null);
    //     NewAdsBoostCheck();
    // }

    public void ActivateCashier()
    {
        RoomActor theRoomPlayerIsIn = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.FindTheRoomThatPlayerIn();
        theRoomPlayerIsIn.roomFixturesOfficer.roomCashier.GetComponent<CashierActor>().CashierWorkerActivate(cashierLiveDuration);
    }

    // public void RandomlyBoostAdsWindow()
    // {
    //     if (lastNewAdsBoostCoroutine != null) StopCoroutine(lastNewAdsBoostCoroutine);
    //     UIManager.instance.boostAdsWindowActor.RandomOpenBoostAdsWindow();
    // }

    #region Button

    [Title("SpeedUpButton")]
    [Button("SpeedUpButton", ButtonSizes.Large)]
    void ButtonSpeedUp()
    {
        SpeedUpTheCustomersForSomeTime(3, 5);
    }
    [Title("FillTheRoomsItemStands")]
    [Button("FillTheRoomsItemStands", ButtonSizes.Large)]
    void ButtonFillTheRoomsItemStands()
    {
        FillTheRoomsItemStands();
    }

    [Title("RandomlyPlaceAPowerBoostBox")]
    [Button("RandomlyPlaceAPowerBoostBox", ButtonSizes.Large)]
    void ButtonRandomlyPlaceAPowerBoostBox()
    {
        RandomlyPlaceAPowerBoostBox();
    }
    #endregion
}
