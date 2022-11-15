using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckArriveInformer : MonoBehaviour
{
    [SerializeField] TruckMoveOfficer truckMoveOfficer;
    [SerializeField] float parkDelay, leftPosDelay;

    [SerializeField] bool triggerBussy = false, isParkPos = false;

    private void OnTriggerStay(Collider other)
    {
        if (other.tag == "Truck" && Time.time > 6.5f)
        {           
            if (!triggerBussy)
            {
                //Debug.Log("Truck entered");
                triggerBussy = true;
                if (isParkPos)
                {
                    StartCoroutine(ArrivedParkPos());
                }
                else
                {
                    StartCoroutine(LeftPosParkPos());
                }
            }
        }            
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Truck")
        {
            if (triggerBussy)
            {
                triggerBussy = false;
            }
        }
    }

    IEnumerator ArrivedParkPos()
    {
        yield return new WaitForSeconds(parkDelay);
        truckMoveOfficer.Parked();
    }

    IEnumerator LeftPosParkPos()
    {
        yield return new WaitForSeconds(leftPosDelay);
        //Debug.Log("LeftPosParkPos");
        truckMoveOfficer.LeftTheMap();
    }
}
