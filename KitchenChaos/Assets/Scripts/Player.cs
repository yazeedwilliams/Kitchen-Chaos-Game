using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Permissions;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float moveSpeed;
    [SerializeField] private float rotationSpeed;

    private void Update()
    {
        Vector2 inputVector = new(0, 0);

        if (Input.GetKey(KeyCode.W))
        {
            inputVector.y = +1;
        }

        if (Input.GetKey(KeyCode.A))
        {
            inputVector.x -= 1;
        }

        if (Input.GetKey(KeyCode.S))
        {
            inputVector.y -= 1;
        }

        if (Input.GetKey(KeyCode.D))
        {
            inputVector.x += 1;
        }

        // keeps the movement speed the same when pressing two directions at once (diagonal movement) as when pressing one direction
        inputVector = inputVector.normalized;

        // uses the inputs from the user and converts it into a vector3 since the transform is vector3
        Vector3 moveDirection = new(inputVector.x, 0f, inputVector.y);

        // moves the player position
        transform.position += moveSpeed * Time.deltaTime * moveDirection;

        // rotates the character to face the direction of movement
        transform.forward = Vector3.Slerp(transform.forward, moveDirection, Time.deltaTime * rotationSpeed);
    }
}
