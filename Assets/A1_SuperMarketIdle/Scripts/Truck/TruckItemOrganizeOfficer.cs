using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class TruckItemOrganizeOfficer : MonoBehaviour
{
    [SerializeField] Transform itemPositions;
    [SerializeField] TruckActor truckActor;
    [SerializeField] List<Transform> truckLuggage = new List<Transform>();
    
    public int truckCapacity = 3;
    

    public void FillTheTruck()
    {
        RefreshThePool();
        //WareHouseOfficer levelsWareHouseOfficer = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelsWareHouseOfficer;
        truckLuggage = truckActor.relatedDepotTruckPointActor.wareHouseOfficer.GetItemsFromThePool(truckCapacity);
        PlaceTheItems();
    }

    void PlaceTheItems()
    {
        for (int i = 0; i < truckLuggage.Count; i++)
        {
            if (i < itemPositions.childCount)
            {
                truckLuggage[i].parent = itemPositions.GetChild(i);
                truckLuggage[i].localPosition = Vector3.zero;
                //truckLuggage[i].position = itemPositions.GetChild(i).position;

            }           
        }

    }

    void RefreshThePool()
    {
        WareHouseOfficer levelsWareHouseOfficer = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>().levelsWareHouseOfficer;
        levelsWareHouseOfficer.PlaceTheObjectsToThePool(truckLuggage);
    }

    public void DeliverTheItems()
    {
        if (truckLuggage.Count > 0)
        {
            StartCoroutine(truckActor.relatedDepotTruckPointActor.itemTakePlaceActor.itemTakePlaceStackOfficer.StockUpTheItems(truckLuggage));
            //truckLuggage = new List<Transform>();
        }
        
    }

    #region Button

    [Title("Fill the truck with items")]
    [Button("Fill the truck", ButtonSizes.Large)]
    void ButtonFillTheTruck()
    {
        FillTheTruck();
    }
    #endregion
}
