using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckMoveOfficer : MonoBehaviour
{
    [SerializeField] TruckClockActor clockActor;
    [SerializeField] TruckActor truckActor;

    public bool isTruckParked = false, isMoving = false;
    float truckPreviousSpeed = 1f;
    [SerializeField] float doorOpenDelay, deliveryDelay;

    public void CallTheTruck()
    {
        truckActor.truckAnimationOfficer.animator.speed = truckPreviousSpeed;
        if (!isTruckParked && !isMoving)
        {
            truckActor.truckAnimationOfficer.TruckCome();
            isMoving = true;
        }
        
    }

    public void SendTheTruck()
    {
        truckActor.truckAnimationOfficer.animator.speed = truckPreviousSpeed;
        if (isTruckParked && !isMoving)
        {           
            clockActor.StartClock();
            truckActor.truckAnimationOfficer.TruckGo();
        }
             
    }

    public void Parked()
    {
        isTruckParked = true;
        isMoving = false;
        truckActor.truckAnimationOfficer.animator.speed = 1f;
        StartCoroutine(OpenTheDoors());
    }

    public void LeftTheMap()
    {
        //print("LEFT the map");
        isTruckParked = false;
        isMoving = false;
        //truckActor.relatedDepotTruckPointActor.belongingRoom.roomTruckOfficer.truckIsReadyToCall = true;
        truckActor.relatedDepotTruckPointActor.belongingRoom.roomTruckOfficer.CallTheTruck(false);

    }

    public void DeliverTheItems()
    {
        truckActor.truckItemOrganizeOfficer.DeliverTheItems();
    }

    public void TruckSpeedSetter(float speed)
    {
        truckPreviousSpeed = speed;
        truckActor.truckAnimationOfficer.animator.speed = speed;
    }

    IEnumerator OpenTheDoors()
    {
        yield return new WaitForSeconds(doorOpenDelay);
        //Debug.Log("Open Door");
        truckActor.truckAnimationOfficer.TruckOpen();
        yield return new WaitForSeconds(deliveryDelay);
        //Debug.Log("Deliver Items");
        DeliverTheItems();
    }
}
