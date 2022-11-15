using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class StuffModelOfficer : MonoBehaviour
{
    [SerializeField] StuffActor stuffActor;
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
        stuffActor.playerAnimationOfficer.animator = modelList[selectedModelIndex].GetComponent<Animator>();
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
