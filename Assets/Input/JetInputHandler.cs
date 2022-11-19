using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class JetInputHandler : MonoBehaviour
{
    [Header("Look")]
    public float liftValue = 0;
    public float steerValue = 0;
    [Space]
    public float xLookSensitivity = 0.5f;
    public float yLookSensitivity = 0.1f;
    [Space]
    public bool invertXLook = false;
    public bool invertYLook = true;

    [Header("Move")]
    public float rollValue = 0;
    public float thrustValue = 0;

    [Header("Fire")]
    public bool firing = false;

    private void Start() {
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void OnLook(InputAction.CallbackContext context) {
        liftValue = context.ReadValue<Vector2>().y * (invertYLook ? -1 : 1);
        steerValue = context.ReadValue<Vector2>().x * (invertXLook ? -1 : 1);
    }
    public void OnMove(InputAction.CallbackContext context) {
        rollValue = context.ReadValue<Vector2>().x;
        thrustValue = context.ReadValue<Vector2>().y;
    }

    public void OnFire(InputAction.CallbackContext context) {
        if (context.performed) firing = true;
        else if (context.canceled) firing = false;
    }
}
