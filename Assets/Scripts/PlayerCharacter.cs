using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCharacter : Unit {
    private InputProcessor inputProcessor = new InputProcessor();
    [SerializeField] private PlayerVariable reference;

    protected override void Awake() {
        base.Awake();
        movable2D.AllowDynamicMovement();
        inputProcessor.Init(ReadMovementInput, unitAttack.Attack);
    }

    private void OnEnable() {
        inputProcessor.Enable();
        reference.Value = this;
    }

    private void ReadMovementInput(InputAction.CallbackContext context) {
        movementDirection = context.ReadValue<Vector2>();
    }

    private void OnDisable() {
        inputProcessor.Disable();
        if (reference.Value == this)
            reference.Value = null;
    }
}
