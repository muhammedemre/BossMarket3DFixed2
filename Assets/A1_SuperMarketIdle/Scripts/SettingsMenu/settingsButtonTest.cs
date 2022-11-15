using System.Collections;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;

public class settingsButtonTest : MonoBehaviour
{
    [SerializeField] bool state = false;
    [SerializeField] Animator animator;

    public void ChangeButtonState()
    {
        state = !state;
        animator.SetBool("State", state);
    }

    #region Button

    [Title("Change Button State")]
    [Button("Change Button State", ButtonSizes.Large)]
    void ButtonChangeButtonState()
    {
       
    }
    #endregion
}
