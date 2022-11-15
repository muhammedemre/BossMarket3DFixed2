using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimationOfficer : MonoBehaviour
{
    [SerializeField] ItemCarryStackOfficer itemCarryStackOfficer;
    public float stuffAnimationSpeed;
    public Animator animator;

    public void PlayIdle()
    {
        if (this.tag == "Stuff")
        {
            animator.speed = 1;
        }
        if (itemCarryStackOfficer.carryingItemsList.Count > 0)
        {
            animator.SetInteger("State", 2);
        }
        else
        {
            animator.SetInteger("State", 0);
        }
        
    }

    public void PlayRun()
    {
        if (this.tag == "Stuff")
        {
            animator.speed = stuffAnimationSpeed;
        }
        if (itemCarryStackOfficer.carryingItemsList.Count > 0)
        {
            animator.SetInteger("State", 3);
        }
        else
        {
            animator.SetInteger("State", 1);
        }
        
    }
}
