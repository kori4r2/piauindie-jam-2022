using UnityEngine;
using UnityEngine.InputSystem;
using System;
using Toblerone.Toolbox;

public class InputProcessor {
    private CustomInputs customInputs;
    Action<InputAction.CallbackContext> movementCallback;
    Action<Vector2> clickCallback;
    private Vector2 currentMousePosition;
    private PointerInputProcessor pointerInputProcessor;

    public void Init(Action<InputAction.CallbackContext> movementCallback, Action<Vector2> clickCallback) {
        customInputs = new CustomInputs();
        pointerInputProcessor = new PointerInputProcessor(customInputs.Combat.Click, customInputs.Combat.MousePosition);
        pointerInputProcessor.OnPress.AddListener(OnClick);
        this.movementCallback = movementCallback;
        this.clickCallback = clickCallback;
    }

    private void OnClick() {
        clickCallback(pointerInputProcessor.PointerPosition);
    }

    public void Enable() {
        customInputs.Enable();
        pointerInputProcessor.Enable();
        AddInputCallbacks();
    }

    private void AddInputCallbacks() {
        customInputs.Combat.Move.performed += movementCallback;
        customInputs.Combat.Move.started += movementCallback;
        customInputs.Combat.Move.canceled += movementCallback;
    }

    public void Disable() {
        RemoveInputCallbacks();
        pointerInputProcessor.Disable();
        customInputs.Disable();
    }

    private void RemoveInputCallbacks() {
        customInputs.Combat.Move.performed -= movementCallback;
        customInputs.Combat.Move.started -= movementCallback;
        customInputs.Combat.Move.canceled -= movementCallback;
    }
}
