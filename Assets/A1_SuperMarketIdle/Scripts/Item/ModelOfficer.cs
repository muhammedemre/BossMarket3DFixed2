using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class ModelOfficer : MonoBehaviour
{
    [SerializeField] List<GameObject> modelList = new List<GameObject>();
    public ItemType selectedType;

    public void SelectTheModel(ItemType selectedModel)
    {
        CloseAll();
        if (modelList.Count > (int)selectedModel)
        {
            modelList[(int)selectedModel].SetActive(true);
            selectedType = selectedModel;
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
    void ButtonSelectTheModel(ItemType selectedModel)
    {
        SelectTheModel(selectedModel);
    }
    #endregion
}
