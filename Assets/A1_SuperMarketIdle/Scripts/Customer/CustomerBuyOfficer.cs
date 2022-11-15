using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerBuyOfficer : MonoBehaviour
{
    [SerializeField] CustomerActor customerActor;
    [SerializeField] Transform shoppingBag;
    [SerializeField] List<ItemActor> boughtItemList = new List<ItemActor>(); // Only to see on inspector
    public int buyCapacity;
    [SerializeField] float buyFrequency;
    float nextBuyCheck;
    bool buying = false;
    ItemStandActor currentItemStandActor;
    [SerializeField] int totalPaymentInBanknotes = 0;

    private void Update()
    {
        if (buying)
        {
            BuyCheck();
        }
    }

    public void BuyItem(ItemStandActor itemStandActor)
    {
        currentItemStandActor = itemStandActor;
        buying = true;
    }

    bool BuyProcess()
    {
        Transform boughtItem = currentItemStandActor.itemStandItemHandleOfficer.SellItem(this);
        if (boughtItem ==  null)
        {
            return false;
        }
        boughtItemList.Add(boughtItem.GetComponent<ItemActor>());
        totalPaymentInBanknotes += customerActor.customerAIOfficer.roomInIt.itemPriceInBanknotesForThisRoom;
        boughtItem.GetComponent<ItemActor>().itemMoveOfficer.ItemGoShoppingBag(shoppingBag);
        buyCapacity--;
        return true;
    }

    void BuyCheck()
    {
        if (nextBuyCheck < Time.time )
        {
            if (buyCapacity > 0)
            {
                nextBuyCheck = Time.time + buyFrequency;
                bool succesfullyTookAnItem = BuyProcess();
                if (!succesfullyTookAnItem)
                {
                    CantBuyAlert();
                }
                else if (customerActor.notificationBoxActor.state)
                {
                    customerActor.customerAIOfficer.roomInIt.roomStuffOrganizeOfficer.ItemStandNeedsRefill(currentItemStandActor.itemStandItemHandleOfficer);
                    customerActor.notificationBoxActor.ActivateOrDeactivateTheNotificationBox(false);
                }
                
            }
            else // buy is complete
            {
                buying = false;
                customerActor.customerAIOfficer.GoToPay();
                currentItemStandActor.busy = false;
            }
        }
    }

    void CantBuyAlert()
    {
        //print(this.name + " CAN'T BUY !!" );
        customerActor.notificationBoxActor.ActivateOrDeactivateTheNotificationBox(true);
    }

    public IEnumerator MakePayment()
    {
        CashierActor cashierToPay = customerActor.customerAIOfficer.roomInIt.roomFixturesOfficer.roomCashier.GetComponent<CashierActor>();
        cashierToPay.moneyHandleOfficer.AddMoneyOnTheDesk(totalPaymentInBanknotes, shoppingBag.position);
        float waitUntilMoneyIsThrown = cashierToPay.moneyHandleOfficer.paymentTotalDuration;
        yield return new WaitForSeconds(waitUntilMoneyIsThrown);
        customerActor.customerAIOfficer.LeaveTheRoom();
    }
}
