using System;
using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour
{
    [SerializeField] private Player player;
    private InputAction changeDirectionAction;
    private InputAction almightyButtonAction;

    private void Start()
    {
        changeDirectionAction = InputSystem.actions["Move"];
        almightyButtonAction = InputSystem.actions["AlmightyButton"];
    }

    private void Update()
    {
        if (changeDirectionAction.ReadValue<float>() > 0)
        {
            player.rightOriented = true;
        }
        else if (changeDirectionAction.ReadValue<float>() < 0)
        {
            player.rightOriented = false;
        }

        if (!almightyButtonAction.WasPressedThisFrame()) return;
        AlmightyButton.instance.OnClick();
    }
}
