using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputSystemManager : MonoBehaviour
{
    public InputAction movementAction;

    private void Awake()
    {
        movementAction = InputSystem.actions["Move"];
    }
}
