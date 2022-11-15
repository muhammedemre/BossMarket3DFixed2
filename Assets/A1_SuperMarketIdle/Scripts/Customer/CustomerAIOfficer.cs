using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class CustomerAIOfficer : MonoBehaviour
{
    [SerializeField] CustomerActor customerActor;
    public RoomActor roomInIt;
    //List<ItemStandActor> activeItemStandsInTheRoom = new List<ItemStandActor>();
    ItemStandActor selectedItemStandActor;

    public enum CustomerState
    {
        Buy, Pay, Leave
    }

    public CustomerState currentState;


    void GoTarget(Vector3 targetPos, Transform lookAtTransfrom)
    {
        customerActor.customerMoveOfficer.MoveToTarget(targetPos, lookAtTransfrom);
       
    }

    public void BuySomething(ItemStandActor _selectedItemStandActor)// First step to buy something
    {
        currentState = CustomerState.Buy;
        selectedItemStandActor = _selectedItemStandActor;
        Vector3 targetPos = selectedItemStandActor.interactionPoint.position;
        GoTarget(targetPos, selectedItemStandActor.transform); // When reaches it calles reachedTheTarget on CustomerMoveOfficer
    }

    public void ReachedTheTarget()
    {
        if (currentState == CustomerState.Buy)
        {
            customerActor.CustomerBuyOfficer.BuyItem(selectedItemStandActor);
        }
        else if (currentState == CustomerState.Pay)
        {
            StartCoroutine(customerActor.CustomerBuyOfficer.MakePayment());
        }
        else if (currentState == CustomerState.Leave)
        {
            LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.RemoveCustomer(customerActor);
            Destroy(gameObject, 0.2f);
        }
    }

    public void GoToPay()
    {
        currentState = CustomerState.Pay;
        Transform cashier = roomInIt.roomFixturesOfficer.roomCashier;
        Vector3 targetPos = cashier.GetComponent<CashierActor>().interactionPoint.position;
        GoTarget(targetPos, cashier); // When reaches it calles reachedTheTarget on CustomerMoveOfficer

    }

    public void LeaveTheRoom()
    {
        currentState = CustomerState.Leave;
        Transform customerSpawnPosition = customerActor.customerAIOfficer.roomInIt.GetComponent<RoomActor>().roomCustomerOfficer.customerSpawnPosition;
        customerActor.customerMoveOfficer.MoveToTarget(customerSpawnPosition.position, customerSpawnPosition);
    }
}
