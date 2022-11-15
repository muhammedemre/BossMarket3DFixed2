using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class StuffMoveOfficer : MonoBehaviour
{
    [SerializeField] StuffActor stuffActor;
    public NavMeshAgent stuff;
    Vector3 movePosition;
    Transform lookAtTransfrom;
    [SerializeField] bool move = false, facing = false, ableFacing = false;
    [SerializeField] float reachTreshold, rotationSmoothTime;
    Vector3 refRotation = Vector3.zero;
    public float speedAtTheBeginning = 0;
    [SerializeField] float currentSpeed;

    private void Start()
    {
        //SetTheSpeed(1);
    }

    private void Update()
    {
        if (stuff.isOnNavMesh && move)
        {
            float remainingDistance = Vector3.Distance(stuff.transform.position, movePosition);
            if (remainingDistance < reachTreshold)
            {
                StopMoving();               
            }
        }
        else if (facing)
        {
            RotateToTheTarget();
        }
    }

    public void MoveToTarget(Vector3 _movePosition, Transform _lookAtTransfrom, bool _ableFacing)
    {
        ableFacing = _ableFacing;
        lookAtTransfrom = _lookAtTransfrom;
        movePosition = _movePosition;
        stuff.SetDestination(movePosition);
        move = true;
        stuffActor.playerAnimationOfficer.PlayRun();
    }

    public void StopMoving()
    {
        move = false;
        stuffActor.playerAnimationOfficer.PlayIdle();
        if (ableFacing)
        {
            facing = true;
        }
        else
        {
            stuffActor.stuffAIOfficer.ReachedTheTarget();
        }
        
    }

    void RotateToTheTarget()
    {
        if (facing)
        {
            Vector3 diffVector = lookAtTransfrom.position - transform.position;
            float angle = Mathf.Atan2(diffVector.x, diffVector.z) * Mathf.Rad2Deg;
            //print("Angle : "+ (int)angle + " transform.eulerAngles.y : " + (int)transform.eulerAngles.y + " :: " + ((int)angle == (int)transform.eulerAngles.y));
            Vector3 targetRot = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRot, ref refRotation, rotationSmoothTime);

            if ((int)angle == (int)transform.eulerAngles.y)
            {
                facing = false;
                ableFacing = false;
                stuffActor.stuffAIOfficer.ReachedTheTarget();
                              
            }
        }
    }

    public void SetTheSpeed(float speedRate)
    {
        //print("SetTheSpeed rate: "+speedRate);
        stuff.speed = speedAtTheBeginning * speedRate;
        currentSpeed = stuff.speed;
    }
}
