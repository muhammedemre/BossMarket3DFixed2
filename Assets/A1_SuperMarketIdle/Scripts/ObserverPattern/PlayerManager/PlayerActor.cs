using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerActor : MonoBehaviour
{
    public PlayerMoveOfficer playerMoveOfficer;
    public PlayerInteractionOfficer playerInteractionOfficer;
    public PlayerAnimationOfficer playerAnimationOfficer;
    public ItemCarryStackOfficer itemCarryStackOfficer;
    public PlayerLevelOfficer playerLevelOfficer;

    private void Awake()
    {
        PlayerManager.instance.playerActor = this;
        AssignPlayerToTheCamera();
    }

    public void ActivateThePlayerProcess()
    {
        playerMoveOfficer.canMove = true;
        // play Idle animation also
        
    }

    public void DeactivateThePlayerProcess()
    {
        
    }

    void AssignPlayerToTheCamera()
    {
        CameraManager.instance.cameraActor.cinemachineVirtualCamera.Follow = transform;
        CameraManager.instance.cameraActor.cinemachineVirtualCamera.LookAt = transform;
    }
}
