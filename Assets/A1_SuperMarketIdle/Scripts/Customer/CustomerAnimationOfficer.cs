using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomerAnimationOfficer : MonoBehaviour
{
    public Animator animator;

    public void PlayIdle()
    {
        animator.SetInteger("State", 0);
    }
    public void PlayWalk()
    {
        animator.SetInteger("State", 1);
    }
}
