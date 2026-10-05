using System;
using UnityEngine;

public class Control : Functionality
{
    private InputManager inputManager = InputManager.Instance;
    private Vector3 inputThisFrame;
    public float moveSpeed=10;

    public override void OnActivate()
    {
        
    }

    public override void OnDeActivate()
    {
    }

    public override void OnUpdate()
    {
        inputThisFrame = inputManager.inputsThisFrame;
        
        transform.Translate(inputThisFrame * (Time.deltaTime * moveSpeed));
    }

    
}
