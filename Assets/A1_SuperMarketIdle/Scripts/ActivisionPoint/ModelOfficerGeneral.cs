using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ModelOfficerGeneral : MonoBehaviour
{
    [SerializeField] List<GameObject> modelList = new List<GameObject>();
    [SerializeField] ActivisionPointAnchor activisionPointAnchor;


    public void SelectTheModel(int selectedModelIndex)
    {
        CloseAll();
        modelList[selectedModelIndex].SetActive(true);
        if (this.tag == "Activision")
        {
            MeshRenderer meshRenderer = modelList[selectedModelIndex].transform.GetChild(0).GetChild(0).GetComponent<MeshRenderer>();
            activisionPointAnchor.ActivisionCalculateOfficer.materialFillMeshRenderer = meshRenderer;
        }
    }

    void CloseAll()
    {
        foreach (GameObject model in modelList)
        {
            model.SetActive(false);
        }
    }

    #region Button

    [Title("Select Model Button")]
    [Button("Select Model", ButtonSizes.Large)]
    void ButtonSelectTheModel(int selectedModelIndex)
    {
        SelectTheModel(selectedModelIndex);
    }
    #endregion
}
