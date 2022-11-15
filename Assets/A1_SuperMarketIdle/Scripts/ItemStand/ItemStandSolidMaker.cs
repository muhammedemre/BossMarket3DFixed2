using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemStandSolidMaker : MonoBehaviour
{
    [SerializeField] BoxCollider colliderToMakeSolid;
    bool solid = false;
    private void OnTriggerExit(Collider other)
    {
        if (!solid)
        {
            if (other.tag == "Player")
            {
                solid = true;
                MakeSolid();
            }
        }
        
    }
    public void MakeSolid()
    {
        colliderToMakeSolid.isTrigger = false;
    }
    public void MakeUnSolid()
    {
        colliderToMakeSolid.isTrigger = true;
    }
}
