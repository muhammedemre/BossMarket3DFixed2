using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WareHouseOfficer : MonoBehaviour
{  
    public List<Transform> itemPoolList = new List<Transform>();
    public List<Transform> usedItemList = new List<Transform>();
    [SerializeField] Transform itemPrefab, itemContainer;
    [SerializeField] int wareHouseCapacity; // # of items in the pool

    private void Start()
    {
        PrepareThePool();      
    }

    void PrepareThePool()
    {
        int itemCounter = 0;
        for (int i = 0; i < wareHouseCapacity; i++)
        {
            Transform tempItem = Instantiate(itemPrefab, itemContainer.position, Quaternion.identity, itemContainer);
            tempItem.gameObject.SetActive(false);
            tempItem.name = "item_" + itemCounter.ToString();
            tempItem.GetComponent<ItemActor>().itemMoveOfficer.relatedWareHouseOfficer = this;
            itemPoolList.Add(tempItem);
            itemCounter++;
        }
    }

    public List<Transform> GetItemsFromThePool(int amount)
    {
        List <Transform> tempItemList = new List<Transform>();
        if (amount < itemPoolList.Count)
        {
            for (int i = 0; i < amount; i++)
            {
                Transform tempItem = GetPoolObjectFromThePool();
                tempItemList.Add(tempItem);
            }
        }
        
        return tempItemList;
    }

    Transform GetPoolObjectFromThePool()
    {
        Transform tempPoolObject = itemPoolList[0];
        tempPoolObject.gameObject.SetActive(true);
        usedItemList.Add(tempPoolObject);
        itemPoolList.RemoveAt(0);
        return tempPoolObject;
    }

    public void PlaceTheObjectsToThePool(List<Transform> poolObjects)
    {
        foreach (Transform item in poolObjects)
        {
            item.gameObject.SetActive(false);
            usedItemList.Remove(item);
            itemPoolList.Add(item);
        }
    }

    public void PlaceASingleObjectToThePool(Transform poolObject)
    {
        poolObject.gameObject.SetActive(false);
        usedItemList.Remove(poolObject);
        itemPoolList.Add(poolObject);
    }

}
