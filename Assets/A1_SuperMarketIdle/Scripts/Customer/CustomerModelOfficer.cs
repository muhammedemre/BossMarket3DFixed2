using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class CustomerModelOfficer : MonoBehaviour
{
    [SerializeField] CustomerActor customerActor;
    [SerializeField] List<GameObject> modelList = new List<GameObject>();

    private void Awake()
    {
        SelectARandomModel();
    }

    public void SelectARandomModel()
    {
        int randomIndex = Random.Range(0, modelList.Count);
        SelectTheModel(randomIndex);
    }

    void SelectTheModel(int selectedModelIndex)
    {
        CloseAll();
        modelList[selectedModelIndex].SetActive(true);
        customerActor.customerAnimationOfficer.animator = modelList[selectedModelIndex].GetComponent<Animator>();
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
