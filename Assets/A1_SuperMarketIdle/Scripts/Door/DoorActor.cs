using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class DoorActor : MonoBehaviour
{
    [SerializeField] Transform doorModel;


    [SerializeField] float rotateDuration;
    bool doorState = false;
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Customer")
        {
            DoorState(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Customer")
        {
            DoorState(false);
        }
    }

    void DoorState(bool state) // true == open
    {
        if (state && !doorState)
        {
            doorState = true;
            doorModel.DORotate(new Vector3(transform.eulerAngles.x, 90f, transform.eulerAngles.z), rotateDuration);
        }
        else if (!state && doorState)
        {
            doorState = false;
            doorModel.DORotate(new Vector3(transform.eulerAngles.x, 0f, transform.eulerAngles.z), rotateDuration);
        }
    }
}
