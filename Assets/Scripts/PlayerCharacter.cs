using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Unit {
    private CustomInputs customInputs;

    protected override void Awake() {
        base.Awake();
        movable2D.AllowDynamicMovement();
        customInputs = new CustomInputs();
    }

    private void OnEnable() {
        customInputs.Enable();
        AddInputCallbacks();
    }

    private void AddInputCallbacks() {
        customInputs.Combat.Move.performed += ReadMovementInput;
        customInputs.Combat.Move.started += ReadMovementInput;
        customInputs.Combat.Move.canceled += ReadMovementInput;
    }

    private void ReadMovementInput(InputAction.CallbackContext context) {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void OnDisable() {
        customInputs.Disable();
        RemoveInputCallbacks();
    }

    private void RemoveInputCallbacks() {
        customInputs.Combat.Move.performed -= ReadMovementInput;
        customInputs.Combat.Move.started -= ReadMovementInput;
        customInputs.Combat.Move.canceled -= ReadMovementInput;
    }
}
