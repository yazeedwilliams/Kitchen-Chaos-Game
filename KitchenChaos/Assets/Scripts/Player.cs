using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;
using UnityEngine.EventSystems;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameInput gameInput;
    [SerializeField] private LayerMask countersLayerMask;

    private bool isWalking;
    private Vector3 lastInteractDirection;
    

    private void Update()
    {
        HandleMovement();
        HandleInteractions();
    }

    public bool IsWalking()
    {
        return isWalking;
    }

    private void HandleInteractions()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // uses the inputs from the user and converts it into a vector3 since the transform is vector3
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        if (moveDirection != Vector3.zero)
        {
            lastInteractDirection = moveDirection;
        }

        float interactDistance = 2f;

        // returns the transform of the rigid body that the raycast hit
        if (Physics.Raycast(transform.position, lastInteractDirection, out RaycastHit raycastHit, interactDistance, countersLayerMask))
        {
            // if the object has a clear counter component
            if (raycastHit.transform.TryGetComponent(out ClearCounter clearCounter))
            {
                clearCounter.Interact();
            }
        } 
    }
    private void HandleMovement()
    {
        Vector2 inputVector = gameInput.GetMovementVectorNormalized();

        // uses the inputs from the user and converts it into a vector3 since the transform is vector3
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        float moveDistance = moveSpeed * Time.deltaTime;
        float playerRadius = 0.7f;
        float playerHeight = 2.0f;

        // returns a boolean of whether the player hits an object or not
        bool canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirection, moveDistance);

        if (!canMove)
        {
            // cannot move towards moveDirection then attempt move only on x axis
            Vector3 moveDirectionX = new Vector3(moveDirection.x, 0, 0).normalized;
            canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionX, moveDistance);

            if (canMove)
            {
                // can only move on the x
                moveDirection = moveDirectionX;
            }
            else
            {
                // cannot move only on the x then attempt move only on z axis
                Vector3 moveDirectionZ = new Vector3(0, 0, moveDirection.z).normalized;
                canMove = !Physics.CapsuleCast(transform.position, transform.position + Vector3.up * playerHeight, playerRadius, moveDirectionZ, moveDistance);

                if (canMove)
                {
                    // can only move on the z
                    moveDirection = moveDirectionZ;
                }
                else
                {
                    //cannot move in any direction
                }
            }
        }

        if (canMove)
        {
            // moves the player position
            transform.position += moveDistance * moveDirection;
        }

        // rotates the character to face the direction of movement
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);

        // the character is walking when the position is not zero
        isWalking = moveDirection != Vector3.zero;
    }
}
