using Sirenix.OdinInspector;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerBoostModelOfficer : MonoBehaviour
{
    [SerializeField] PowerBoostBoxActor powerBoostBoxActor;

    [SerializeField] List<GameObject> modelList = new List<GameObject>();

    public enum PowerBoostType
    {
        // GiftBox, 
        Case
    }

    private void Awake()
    {
        SelectARandomModel();
    }

    public void SelectARandomModel()
    {
        int randomIndex = Random.Range(0, modelList.Count);
        SelectTheModel((PowerBoostType)randomIndex);
    }

    void SelectTheModel(PowerBoostType type)
    {
        CloseAll();
        modelList[(int)type].SetActive(true);
        powerBoostBoxActor.selectedBoostType = type;
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
    void ButtonSelectTheModel(PowerBoostType type)
    {
        SelectTheModel(type);
    }

    [Title("Select Random Model Button")]
    [Button("Select Random Model", ButtonSizes.Large)]
    void ButtonRandomSelectTheModel()
    {
        SelectARandomModel();
    }
    #endregion
}
