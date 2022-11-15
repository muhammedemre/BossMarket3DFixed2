using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomCustomerOfficer : MonoBehaviour
{
    [SerializeField] RoomActor roomActor;
    [SerializeField] float startSendingCustomersAfterThisWile, itemStandStateCheckFrequency, customerSendFrequency;
    [SerializeField] float nextItemStandStateCheck, nextCustomerSendCheck;
    [SerializeField] Transform customerPrefab, customerContainer;
    public Transform customerSpawnPosition;
    [SerializeField] List<ItemStandActor> itemStandActorsQueue = new List<ItemStandActor>();
    int customerCount = 0;


    private void Awake()
    {
        nextItemStandStateCheck = Time.time + startSendingCustomersAfterThisWile;
        nextCustomerSendCheck = Time.time + startSendingCustomersAfterThisWile;
    }

    private void Update()
    {
        ItemStandStateCheck();
        if (Time.time > UIManager.instance.splashVideoDuration)
        {
            ItemStandStateCheck();
            CustomerSendCheck();
        }
        
    }

    void ItemStandStateCheck()
    {
        if (nextItemStandStateCheck > Time.time)
        {
            nextItemStandStateCheck = Time.time + itemStandStateCheckFrequency;
            foreach (Transform itemStands in roomActor.roomFixturesOfficer.roomItemStands)
            {
                if (!itemStandActorsQueue.Contains(itemStands.GetComponent<ItemStandActor>()))
                {
                    if (itemStands.GetComponent<ItemStandActor>().busy == false)
                    {
                        itemStandActorsQueue.Add(itemStands.GetComponent<ItemStandActor>());
                    }                  
                }
            }
        }
        
    }

    void CustomerSendCheck()
    {
        if (nextCustomerSendCheck < Time.time)
        {
            nextCustomerSendCheck = Time.time + customerSendFrequency;
            if (itemStandActorsQueue.Count > 0)
            {
                CustomerActor currentCustomer = CreateACustomer();
                LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelRoomOfficer.RegisterCustomer(currentCustomer);
                currentCustomer.customerAIOfficer.BuySomething(itemStandActorsQueue[0]);
                itemStandActorsQueue[0].busy = true;
                itemStandActorsQueue.RemoveAt(0);
            }
        }
    }

    CustomerActor CreateACustomer()
    {
        Transform tempCustomer = Instantiate(customerPrefab, customerSpawnPosition.position, Quaternion.identity, customerContainer);
        tempCustomer.name = "Customer_" + customerCount.ToString();
        tempCustomer.GetComponent<CustomerActor>().customerAIOfficer.roomInIt = roomActor;
        customerCount++;
        return tempCustomer.GetComponent<CustomerActor>();
    }
}
