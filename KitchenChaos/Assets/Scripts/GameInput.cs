using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();
    }
    public Vector2 GetMovementVectorNormalized()
    {
        // accessing the input actions from the player input actions
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        // keeps the movement speed the same when pressing two directions at once (diagonal movement) as when pressing one direction
        inputVector = inputVector.normalized;

        return inputVector;
    }
}
