using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

public class LevelNavmeshOfficer : MonoBehaviour
{

    public List<NavMeshSurface> surfaces = new List<NavMeshSurface>();

    public void BuildTheNavmeshes()
    {
        for (int i = 0; i < surfaces.Count; i++)
        {
            //surfaces[i].BuildNavMesh(); PreNavmesh is on using
        }
    }


    #region Button

    [Title("Build The Navmesh")]
    [Button("Build Navmesh", ButtonSizes.Large)]
    void ButtonBuildTheNavmesh()
    {
        BuildTheNavmeshes();
    }
    #endregion
}

