using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomEnvironmentOfficer : MonoBehaviour
{
    [SerializeField] List<MeshRenderer> roomWallList = new List<MeshRenderer>();
    [SerializeField] List<MeshRenderer> floorList = new List<MeshRenderer>();
    [SerializeField] List<Material> wallMatList = new List<Material>();
    [SerializeField] List<Material> floorMatList = new List<Material>();

    public void UpgradeWall(int upgradeTo)
    {
        foreach (MeshRenderer meshRenderer in roomWallList)
        {
            meshRenderer.material = wallMatList[upgradeTo];
        }
    }
    public void UpgradeFloor(int upgradeTo)
    {
        foreach (MeshRenderer meshRenderer in floorList)
        {
            meshRenderer.material = floorMatList[upgradeTo];
        }
    }
}
