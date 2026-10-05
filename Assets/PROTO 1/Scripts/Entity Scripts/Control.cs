using System;
using UnityEngine;

public class Control : Functionality
{
    public InputManager inputManager;
    private Vector3 inputThisFrame;
    public float moveSpeed=10;
    private void Start()
    {
        inputManager = FindAnyObjectByType<InputManager>();
    }

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
