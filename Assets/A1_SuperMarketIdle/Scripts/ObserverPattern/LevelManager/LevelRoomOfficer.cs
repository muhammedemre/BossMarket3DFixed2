using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class LevelRoomOfficer : SerializedMonoBehaviour
{
    [SerializeField] LevelActor levelActor;
    //public List<Transform> levelRooms = new List<Transform>();
    public Dictionary<int, GameObject> levelRooms = new Dictionary<int, GameObject>();
    public Dictionary<int, GameObject> levelRoomActivators = new Dictionary<int, GameObject>();
    public List<CustomerActor> activeCustomersInLevel = new List<CustomerActor>();
    //int nextRoomIndex = 0;


    public void ActivateARooom(int roomIndex)
    {
        levelRoomActivators[roomIndex].GetComponent<ActivisionPointAnchor>().ActivisionCalculateOfficer.ActivateTheObject();
    }

    public void RegisterCustomer(CustomerActor customer) 
    {
        activeCustomersInLevel.Add(customer);
    }

    public void RemoveCustomer(CustomerActor customer) 
    {
        activeCustomersInLevel.Remove(customer);
    }

    public RoomActor FindTheRoomThatPlayerIn() 
    {
        RoomActor closestRoom = null;
        float closestDistance = 55555f;
        foreach (int roomIndex in levelRooms.Keys)
        {
            if (Vector3.Distance(PlayerManager.instance.playerActor.transform.position, levelRooms[roomIndex].transform.position) < closestDistance)
            {
                closestDistance = Vector3.Distance(PlayerManager.instance.playerActor.transform.position, levelRooms[roomIndex].transform.position);
                closestRoom = levelRooms[roomIndex].GetComponent<RoomActor>();
            }
        }
        return closestRoom;
    }
}
