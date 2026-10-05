using System;
using UnityEngine;

public class Control : Functionality
{
    public HolderOfSingleTon holderOfSingleTon;
    private Vector3 inputThisFrame;
    public float moveSpeed = 10;

    public override void OnActivate()
    {
        
    }

    public override void OnDeActivate()
    {
        
    }

    public override void OnUpdate()
    {
        inputThisFrame = holderOfSingleTon.inputManager.inputsThisFrame;
        transform.Translate(inputThisFrame * (Time.deltaTime * moveSpeed));
    }
}
