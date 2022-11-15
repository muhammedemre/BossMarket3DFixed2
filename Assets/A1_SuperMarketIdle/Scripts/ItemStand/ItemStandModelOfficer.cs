using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Sirenix.OdinInspector;

public class ItemStandModelOfficer : SerializedMonoBehaviour
{
    [SerializeField] ItemStandActor itemStandActor;
    [SerializeField] List<MeshRenderer> itemStandModelMeshes = new List<MeshRenderer>();
    [SerializeField] List<GameObject> BoardList = new List<GameObject>();
    [SerializeField] List<GameObject> itemIconList = new List<GameObject>(); // order should be same with ItemType's order
    [SerializeField] List<Transform> itemPositionContainerList = new List<Transform>();
    [SerializeField] List<Transform> ParticleContainerList = new List<Transform>();
    [SerializeField] List<ItemType> itemsOnWoodenItemStand = new List<ItemType>();
    [SerializeField] List<ItemType> itemsOnClothStand = new List<ItemType>();


    public void SelectTheModel(ItemType selectedModel)
    {
        CloseAll();
        ActivateStandModel(selectedModel);
        ActivateItemIcon(selectedModel);
        itemStandActor.itemStandItemHandleOfficer.standsItemType = selectedModel;
    }

    void CloseAll()
    {
        DeActivateStandModel();
        DeActivateItemIcon();
    }

    void ActivateStandModel(ItemType type) 
    {
        if (itemsOnWoodenItemStand.Contains(type))
        {
            itemStandModelMeshes[0].enabled = true;
            BoardList[0].SetActive(true);
            itemStandActor.itemStandItemHandleOfficer.AssignItemPositions(itemPositionContainerList[0]);
            itemStandActor.itemStandItemHandleOfficer.AssignParticleContainer(ParticleContainerList[0]);
        }
        else if (itemsOnClothStand.Contains(type))
        {
            itemStandModelMeshes[1].enabled = true;
            BoardList[1].SetActive(true);
            itemStandActor.itemStandItemHandleOfficer.AssignItemPositions(itemPositionContainerList[1]);
            itemStandActor.itemStandItemHandleOfficer.AssignParticleContainer(ParticleContainerList[1]);
        }
    }

    void ActivateItemIcon(ItemType type) 
    {
        int itemIndex = (int)type;
        itemIconList[itemIndex].SetActive(true);
    }

    void DeActivateStandModel()
    {
        foreach (MeshRenderer modelMesh in itemStandModelMeshes)
        {
            modelMesh.enabled = false;
        }
        foreach (GameObject board in BoardList)
        {
            board.SetActive(false);
        }
    }

    void DeActivateItemIcon()
    {
        foreach (GameObject iconModel in itemIconList)
        {
            iconModel.SetActive(false);
        }
    }

    #region Button

    [Title("Select Model Button")]
    [Button("Select Model", ButtonSizes.Large)]
    void ButtonSelectTheModel(ItemType selectedModel)
    {
        SelectTheModel(selectedModel);
    }
    #endregion
}
