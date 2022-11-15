using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TruckAnimationOfficer : MonoBehaviour
{
    public Animator animator;

    public void TruckCome()
    {
        animator.SetInteger("State", 1);
    }

    public void TruckGo()
    {
        animator.SetInteger("State", 2);
    }

    public void TruckOpen()
    {
        animator.SetInteger("State", 3);
    }

}
