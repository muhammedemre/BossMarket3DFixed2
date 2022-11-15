using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeTabActor : MonoBehaviour
{
    [SerializeField] GameObject relatedTabSpriteObject;
    [SerializeField] bool tabIsActivated = false;
    [SerializeField] Sprite activeSprite, deactiveSprite;

    public void ActivateOrDeactivateTheTab(bool state)
    {
        relatedTabSpriteObject.GetComponent<Image>().sprite = (state) ? activeSprite : deactiveSprite;
        gameObject.SetActive(state);
    }

}
