using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpgradePointInteractionOfficer : MonoBehaviour
{
    [SerializeField] UpgradePointActor upgradePointActor;
    [SerializeField] DepotTruckPointActor depotTruckPointActor;
    [SerializeField] UpgradePointType selectedUpgradePointType;
    public bool visited = false;

    enum UpgradePointType
    {
        Center, Truck
    }
    bool busyWithPlayer = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player" && !busyWithPlayer)
        {
            busyWithPlayer = true;
            visited = true;
            if (selectedUpgradePointType == UpgradePointType.Center)
            {
                upgradePointActor.OpenUpgradeWindow();
            }
            else if (selectedUpgradePointType == UpgradePointType.Truck)
            {
                depotTruckPointActor.truckHandleOfficer.OpenTruckUpgradeWindow();
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player" && busyWithPlayer)
        {
            busyWithPlayer = false;
        }
    }
}
