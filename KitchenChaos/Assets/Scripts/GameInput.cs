using System;
using UnityEngine;

public class GameInput : MonoBehaviour
{
    public event EventHandler OnInteractAction;
    private PlayerInputActions playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputActions();
        playerInputActions.Player.Enable();

        // listen for the performed event
        playerInputActions.Player.Interact.performed += Interact_performed;
    }

    private void Interact_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        OnInteractAction?.Invoke(this, EventArgs.Empty);
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
