
using UnityEngine;

public class GameInput : MonoBehaviour
{
    private PlayerInputAction playerInputActions;

    private void Awake()
    {
        playerInputActions = new PlayerInputAction();
        playerInputActions.Player.Enable();
    }

    public Vector2 GetMovementVectorNormalized()
    {
        Vector2 inputVector = playerInputActions.Player.Move.ReadValue<Vector2>();

        inputVector = inputVector.normalized;
        return inputVector;
    }
}
