using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMoveOfficer : MonoBehaviour
{
    public float speed, moveTreshold, magnitudeNormalizer, speedAtTheBeginning;
    public bool canMove;
    [SerializeField] CharacterController characterController;

    private void Start()
    {
        speedAtTheBeginning = speed;
    }

    public void MoveTheCharacter(Vector3 moveVector)
    {
        if (moveVector.magnitude > moveTreshold)
        {
            Vector3 direction = moveVector.normalized;
            direction = -direction;
            characterController.Move(direction * speed * (moveVector.magnitude * magnitudeNormalizer) * Time.deltaTime);
            RotationOfTheCharacter(direction);
            PlayerManager.instance.playerActor.playerAnimationOfficer.PlayRun();
        }
        else
        {
            PlayerManager.instance.playerActor.playerAnimationOfficer.PlayIdle();
        }
        transform.position = new Vector3(transform.position.x, 0f, transform.position.z);
    }

    void RotationOfTheCharacter(Vector3 direction)
    {
        float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(transform.eulerAngles.x, targetAngle, transform.eulerAngles.z);
    }

}
