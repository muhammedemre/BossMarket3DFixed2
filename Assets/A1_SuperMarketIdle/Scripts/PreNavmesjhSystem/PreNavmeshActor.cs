using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PreNavmeshActor : MonoBehaviour
{
    [SerializeField] List<NavMeshSurface> navmeshSurfaces = new List<NavMeshSurface>();

    private void Start()
    {
        
    }

    void BuildNavmesh()
    {
        foreach (NavMeshSurface surface in navmeshSurfaces)
        {
            //surface.BuildNavMesh();
        }
    }
}
