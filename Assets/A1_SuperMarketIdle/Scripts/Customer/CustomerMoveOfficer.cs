using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class CustomerMoveOfficer : MonoBehaviour
{
    [SerializeField] CustomerActor customerActor;
    public NavMeshAgent customer;
    [SerializeField] Vector3 movePosition;
    Transform lookAtTransfrom;
    [SerializeField] bool move = false, facing = false;
    [SerializeField] float reachTreshold, rotationSmoothTime;
    Vector3 refRotation = Vector3.zero;

    private void Update()
    {
        if (customer.isOnNavMesh && move)
        {
            float remainingDistance = Vector3.Distance(customer.transform.position, movePosition);
            if (remainingDistance < reachTreshold)
            {
                StopMoving();
                if (!(customerActor.customerAIOfficer.currentState == CustomerAIOfficer.CustomerState.Leave))
                {
                    facing = true;
                }
                else
                {
                    customerActor.customerAIOfficer.ReachedTheTarget();
                }
                
                //customerActor.customerAIOfficer.ReachedTheTarget();
            }
        }
        else if (facing)
        {
            RotateToTheTarget();
        }
    }

    public void MoveToTarget(Vector3 _movePosition, Transform _lookAtTransfrom)
    {
        lookAtTransfrom = _lookAtTransfrom;
        movePosition = _movePosition;
        customer.SetDestination(movePosition);
        move = true;
        customerActor.customerAnimationOfficer.PlayWalk();
    }

    public void StopMoving()
    {
        move = false;
        customerActor.customerAnimationOfficer.PlayIdle();
    }

    void RotateToTheTarget()
    {
        if (facing)
        {
            Vector3 diffVector = lookAtTransfrom.position - transform.position;
            float angle = Mathf.Atan2(diffVector.x , diffVector.z) * Mathf.Rad2Deg;
            //print("Angle : "+ (int)angle + " transform.eulerAngles.y : " + (int)transform.eulerAngles.y + " :: " + ((int)angle == (int)transform.eulerAngles.y));
            Vector3 targetRot = new Vector3(transform.eulerAngles.x, angle, transform.eulerAngles.z);
            transform.eulerAngles = Vector3.SmoothDamp(transform.eulerAngles, targetRot, ref refRotation, rotationSmoothTime);

            if ((int)angle == (int)transform.eulerAngles.y )
            {
                facing = false;
                customerActor.customerAIOfficer.ReachedTheTarget();
            }
        }
    }

    public void SetTheCustomerSpeed(float speed) 
    {
        customer.speed = speed;
    }
}
