using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;
using Sirenix.OdinInspector;

public class RoomFixturesOfficer : SerializedMonoBehaviour
{
    public List<Transform> roomItemStands = new List<Transform>();
    public Dictionary<int, GameObject> roomItemStandActivator = new Dictionary<int, GameObject>();
    public List<NavMeshSurface> roomActiveNavmeshSurfaces = new List<NavMeshSurface>();
    public Transform roomUpgradePoint, roomCashier, depotTruckPoint;

    public void AssignYourActiveNavmeshSurfacesToLevelNavmeshSurfaces(LevelNavmeshOfficer levelNavmeshOfficer)
    {
        foreach (NavMeshSurface surface in roomActiveNavmeshSurfaces)
        {
            if (!levelNavmeshOfficer.surfaces.Contains(surface))
            {
                levelNavmeshOfficer.surfaces.Add(surface);
            }          
        }       
    }

    public void AddNavmeshSurfaceToTheActiveNavmeshSurfaces(NavMeshSurface navMeshSurface)
    {
        roomActiveNavmeshSurfaces.Add(navMeshSurface);
        ActivateNavmeshSurfaceOnTheRoom();
    }
    public void ActivateNavmeshSurfaceOnTheRoom()
    {       
        LevelActor currentLevelActor = LevelManager.instance.levelCreateOfficer.currentLevel.GetComponent<LevelActor>();
        AssignYourActiveNavmeshSurfacesToLevelNavmeshSurfaces(currentLevelActor.levelNavmeshOfficer);
        currentLevelActor.levelNavmeshOfficer.BuildTheNavmeshes();
    }
}
