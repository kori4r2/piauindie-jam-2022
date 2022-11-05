using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Unit {
    private CustomInputs customInputs;
    private Vector2 movementDirection;
    [SerializeField, Range(0f, 20f)] private float moveSpeed = 5f;
    [SerializeField] private PlayerVariable reference;

    protected override void Awake() {
        base.Awake();
        movable2D.AllowDynamicMovement();
        customInputs = new CustomInputs();
    }

    private void OnEnable() {
        customInputs.Enable();
        AddInputCallbacks();
        reference.Value = this;
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
        if (reference.Value == this)
            reference.Value = null;
    }

    private void RemoveInputCallbacks() {
        customInputs.Combat.Move.performed -= ReadMovementInput;
        customInputs.Combat.Move.started -= ReadMovementInput;
        customInputs.Combat.Move.canceled -= ReadMovementInput;
    }

    protected override void ProcessMovementInput() {
        movable2D.SetVelocity(moveSpeed * movementDirection);
    }
}
