using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NotificationBoxActor : MonoBehaviour
{
    [SerializeField] GameObject notificationBox;
    [SerializeField] Animator animator;
    public bool state = false;

    private void Update()
    {
        if (state)
        {
            transform.LookAt(Camera.main.transform);
        }       
    }

    public void ActivateOrDeactivateTheNotificationBox(bool _state)
    {
        state = _state;
        int stateIndex = _state ? 1 : 0;
        animator.SetInteger("State", stateIndex);
    }

    public void ActivateTheBox()
    {
        notificationBox.SetActive(true);
    }
    public void DeactivateTheBox()
    {
        notificationBox.SetActive(false);
    }
}
